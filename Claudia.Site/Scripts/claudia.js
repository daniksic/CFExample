// Avoid `console` errors in browsers that lack a console.
if (!(window.console && console.log)) {
    (function () {
        var noop = function () { };
        var methods = ['assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error', 'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log', 'markTimeline', 'profile', 'profileEnd', 'markTimeline', 'table', 'time', 'timeEnd', 'timeStamp', 'trace', 'warn'];
        var length = methods.length;
        var console = window.console = {};
        while (length--) {
            console[methods[length]] = noop;
        }
    }());
}
/* Facebook */
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=487185038068614";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
/* Twitter */
!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");
/* Google +*/
(function () {
    var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
    po.src = 'https://apis.google.com/js/platform.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
})();
//Internet Explorer 10 in Windows 8 and Windows Phone 8 - bug fix
if (navigator.userAgent.match(/IEMobile\/10\.0/)) {
    var msViewportStyle = document.createElement('style')
    msViewportStyle.appendChild(
      document.createTextNode(
        '@-ms-viewport{width:auto!important}'
      )
    )
    document.querySelector('head').appendChild(msViewportStyle)
}
//$(function () {
//    function initToolbarBootstrapBindings() {
//        var fonts = ['Serif', 'Sans', 'Arial', 'Arial Black', 'Courier',
//              'Courier New', 'Comic Sans MS', 'Helvetica', 'Impact', 'Lucida Grande', 'Lucida Sans', 'Tahoma', 'Times',
//              'Times New Roman', 'Verdana'],
//              fontTarget = $('[title=Font]').siblings('.dropdown-menu');
//        $.each(fonts, function (idx, fontName) {
//            fontTarget.append($('<li><a data-edit="fontName ' + fontName + '" style="font-family:\'' + fontName + '\'">' + fontName + '</a></li>'));
//        });
//        $('a[title]').tooltip({ container: 'body' });
//        $('.dropdown-menu input').click(function () { return false; })
//            .change(function () { $(this).parent('.dropdown-menu').siblings('.dropdown-toggle').dropdown('toggle'); })
//        .keydown('esc', function () { this.value = ''; $(this).change(); });

//        $('[data-role=magic-overlay]').each(function () {
//            var overlay = $(this), target = $(overlay.data('target'));
//            overlay.css('opacity', 0).css('position', 'absolute').offset(target.offset()).width(target.outerWidth()).height(target.outerHeight());
//        });
//        if ("onwebkitspeechchange" in document.createElement("input")) {
//            var editorOffset = $('#editor').offset();
//            $('#voiceBtn').css('position', 'absolute').offset({ top: editorOffset.top, left: editorOffset.left + $('#editor').innerWidth() - 35 });
//        } else {
//            $('#voiceBtn').hide();
//        }
//    };
//    function showErrorAlert(reason, detail) {
//        var msg = '';
//        if (reason === 'unsupported-file-type') { msg = "Unsupported format " + detail; }
//        else {
//            console.log("error uploading file", reason, detail);
//        }
//        $('<div class="alert"> <button type="button" class="close" data-dismiss="alert">&times;</button>' +
//         '<strong>File upload error</strong> ' + msg + ' </div>').prependTo('#alerts');
//    };
//    initToolbarBootstrapBindings();
//    $('#editor').wysiwyg({ fileUploadError: showErrorAlert });
//    window.prettyPrint && prettyPrint();
//});