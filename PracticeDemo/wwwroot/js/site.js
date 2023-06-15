$(function () {
    if ($('div.alert.notification').length) {
        setTimeout(() => {

            $('div.alert.notification').fadeOut();

        }, 3000)
    }
})