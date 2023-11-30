



(function() {
    'use strict'

    //檢測時間間隔
    var timer = setInterval(() => {
        var ad = document.querySelector('ytp-ad-skip-button-modern ytp-button');
        if (ad) {
            ad.click();
            console.log('skip ad');
        }
    }, 1000);
})();