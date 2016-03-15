/*****************************************************************************
 *
 * @author 郑志强
 * 
 * @requires jQuery zq 
 * 
 * @description  input file 在浏览器中显示图片的扩展。目前只支持IE 9,11 谷歌 火狐 。IE10未测试。 
 *
 *****************************************************************/


var zq = (function ($, zq) {

    zq.fileShowImg = function (options) {
        var opts = $.extend({}, {
            $target: $('input[type="file"]'),
            $imgTarget:null,
            imgWidth: "200",
            imgHeight: "200",
            changedAfter: function (img) { }
        }, options);
        opts.$target.on("change", function () {
            var $file = $(this);
            var $img = opts.$imgTarget || $($file.prev());;
            //if (imgDom[0].tagName === "IMG") {
            //    $img = $(imgDom[0]);
            //} else {
            //    $img = $('<img  alt=""/>');
            //}
            //$img.width(opts.imgWidth).height(opts.imgHeight);
            var ext = $file.val().substring($file.val().lastIndexOf(".") + 1).toLowerCase();
            // gif在IE浏览器暂时无法显示
            if (ext !== "png" && ext !== "jpg" && ext !== "jpeg") {
                alert("图片的格式必须为png或者jpg或者jpeg格式！");
                return;
            }
            // IE浏览器
            if (document.all) {
                $file[0].select();
                var reallocalpath = document.selection.createRange().text;
                var ie6 = /msie 6/i.test(navigator.userAgent);
                // IE6浏览器设置img的src为本地路径可以直接显示图片
                if (ie6) $img[0].src = reallocalpath;
                else {
                    // 非IE6版本的IE由于安全问题直接设置img的src无法显示本地图片，但是可以通过滤镜来实现
                    $img[0].style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='image',src=\"" + reallocalpath + "\")";
                    // 设置img的src为base64编码的透明图片 取消显示浏览器默认图片
                    $img[0].src = "data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==";
                }
            } else {
                html5Reader($file, $img);
            }
            //$file.before($img);

            opts.changedAfter($img);
        });

        function html5Reader($file, $img) {
            var f = $file[0].files[0];
            var reader = new FileReader();
            reader.readAsDataURL(f);
            reader.onload = function (e) {
                $img.attr("src", this.result);
            }
        }
    }

    return zq;

})($, zq);
