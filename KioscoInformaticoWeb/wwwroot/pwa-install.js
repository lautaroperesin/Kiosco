// wwwroot/pwa-install.js
(function () {
    let deferredPrompt = null;
    let dotnetHelper = null;

    function isStandaloneDisplayMode() {
        if (window.matchMedia && window.matchMedia('(display-mode: standalone)').matches) return true;
        if (navigator.standalone === true) return true; // iOS
        if (document.referrer && document.referrer.startsWith('android-app://')) return true;
        return false;
    }

    // REGISTRO INMEDIATO (evita perder el evento si llega temprano)
    window.addEventListener('beforeinstallprompt', (e) => {
        e.preventDefault();
        deferredPrompt = e;
        // Si el helper ya está, avisamos al componente para que muestre el botón
        if (dotnetHelper) {
            dotnetHelper.invokeMethodAsync('OnCanInstallChanged', true);
        }
    });

    window.addEventListener('appinstalled', () => {
        deferredPrompt = null;
        if (dotnetHelper) {
            dotnetHelper.invokeMethodAsync('OnInstalled');
        }
    });

    // Safari viejo no soporta addEventListener en MediaQueryList
    try {
        const mql = window.matchMedia('(display-mode: standalone)');
        const onChange = (evt) => dotnetHelper?.invokeMethodAsync('OnInstallStateChanged', !!evt.matches);
        if (mql && (typeof mql.addEventListener === 'function')) {
            mql.addEventListener('change', onChange);
        } else if (mql && (typeof mql.addListener === 'function')) {
            mql.addListener(onChange);
        }
    } catch { /* no-op */ }

    window.pwaInstall = {
        // ahora init SOLO recibe el helper (los listeners ya están)
        init: (helper) => {
            dotnetHelper = helper;
            // Si el evento ya ocurrió antes de init, reflejamos estado actual:
            const canInstallNow = !!deferredPrompt && !isStandaloneDisplayMode();
            helper.invokeMethodAsync('OnCanInstallChanged', canInstallNow);
            if (isStandaloneDisplayMode()) {
                helper.invokeMethodAsync('OnInstallStateChanged', true);
            }
        },

        isInstalled: () => isStandaloneDisplayMode(),

        canInstall: () => !!deferredPrompt && !isStandaloneDisplayMode(),

        promptInstall: async () => {
            if (!deferredPrompt) return 'unavailable';
            deferredPrompt.prompt();
            const choice = await deferredPrompt.userChoice;
            // Solo se puede usar una vez
            deferredPrompt = null;
            dotnetHelper?.invokeMethodAsync('OnCanInstallChanged', false);
            return choice.outcome; // 'accepted' | 'dismissed'
        },

        isIOS: () => /iphone|ipad|ipod/i.test(navigator.userAgent),
        isSafari: () => /^((?!chrome|android).)*safari/i.test(navigator.userAgent)
    };
})();
