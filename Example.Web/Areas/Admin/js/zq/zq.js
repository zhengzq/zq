/*****************************************************************************
 *
 * @author 郑志强
 * 
 * @requires jQuery
 * 
 * @description 项目js的核心对象
 *
 *****************************************************************/

var zq = (function ($, window) {

    var zq = function ($, window) { };
    /**
     * 对象的基本属性
     * 
     */
    zq.fn = zq.prototype = {
        topJQuery: top.$,
        topWindow: top.window,
        parentJQuery: parent ? null : parent.$,
        parentWindow: parent ? null : parent.window
    };

    return new zq($, window);

})($, window);
/**
 * zq的对象的一些基础工具
 * 
 * @author 郑志强
 *
 * @requires  zq 
 */
zq = (function (zq) {
    /**
     * 请求下载文件
     * 
     * @params url  请求链接
     * @params data 传递的参数对象
     * @params method 请求的方式(默认值为post请求)
     */
    zq.download = function (url, data, method) {
        if (typeof (data) === "string" && data.length > 0) {
            $.error("data must not be serialized");
        }
        method = method || "post";
        var form = $("<form enctype='multipart/form-data' method='" + method + "'></form>")
            .hide()
            .attr({ action: url, target: "_self" });

        $.each(data || {}, function (name, value) {
            if ($.isPlainObject(value)) {
                name = value.name;
                value = value.value;
            }
            $("<input type='hidden' />").attr({ name: name, value: value }).appendTo(form);
        });
        $("<input type='hidden' value='IFrame' name='X-Requested-With' />").appendTo(form);

        $("body").append(form);
        form[0].submit();
    };

    zq.format = {
        booleanFormater: function (value) {
            return value === true ? "是" : "否";
        },
        /**
         * 格式化时间
         * 
         * @param str 需处理时间字符串
         * @comment 逻辑是处理带T的时间字符串格式
         * @returns 格式化后时间字符串
         */
        timespFormater: function (value) {
            var day = (value / (24 * 60 * 60)).toFixed(0).toString();
            var hour = ((value - day * (24 * 60 * 60)) / (60 * 60)).toFixed(0).toString();
            var second = ((value - day * (24 * 60 * 60) - hour * (60 * 60)) / 60).toFixed(0).toString();
            return day + "天" + ("00" + hour).substr(hour.length, 2) + "时" + ("00" + second).substr(second.length, 2) + "分";
        },
        /**
         * 格式化时间轴
         * 
         * @param value 需处理时间字符串
         * @comment 逻辑是处理带T的时间字符串格式
         * @returns 格式化后时间字符串  [example:2010年12月23日 10:53]
         */
        timelineFormatter: function (value) {
            return eval("new " + (value.replace(/\//g, ""))).toLocaleString().replace(/:\d{1,2}$/, " ");
        },

        timeFormatter: function (value) {
            if (value) {
                var ts = value.split("T");
                if (ts[0] === "0001-01-01") // JJ 2015-07-14 09:23:00
                    return "";
                else
                    return ts[0] + " " + ts[1].replace("Z", "");
            }
            return "";
        }


    }

    return zq;
})(zq);
/**
 * zq的对象对JS的扩展
 * 
 * @author 郑志强
 *
 * @requires  zq 
 */
zq = (function ($, zq) {
    /**
     * 序列化扩展
     * 
     */
    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || "");
            } else {
                o[this.name] = this.value || "";
            }
        });
        return o;
    };
    /**
     * 
     * 
     */
    String.prototype.format = function () {
        if (arguments.length == 0)
            return null;
        var str =this;
        for (var i = 0; i < arguments.length; i++) {
            var re = new RegExp("\\{" + i + '\\}', 'gm');
            str = str.replace(re, arguments[i]);
        }
        return str;
    };

    Date.prototype.Format = function (formatStr) {
        var str = formatStr;
        var week = ['日', '一', '二', '三', '四', '五', '六'];

        str = str.replace(/yyyy|YYYY/, this.getFullYear());
        str = str.replace(/yy|YY/, (this.getYear() % 100) > 9 ? (this.getYear() % 100).toString() : '0' + (this.getYear() % 100));

        str = str.replace(/MM/, this.getMonth() > 9 ? this.getMonth().toString() : '0' + this.getMonth());
        str = str.replace(/M/g, this.getMonth());

        str = str.replace(/w|W/g, week[this.getDay()]);

        str = str.replace(/dd|DD/, this.getDate() > 9 ? this.getDate().toString() : '0' + this.getDate());
        str = str.replace(/d|D/g, this.getDate());

        str = str.replace(/hh|HH/, this.getHours() > 9 ? this.getHours().toString() : '0' + this.getHours());
        str = str.replace(/h|H/g, this.getHours());
        str = str.replace(/mm/, this.getMinutes() > 9 ? this.getMinutes().toString() : '0' + this.getMinutes());
        str = str.replace(/m/g, this.getMinutes());

        str = str.replace(/ss|SS/, this.getSeconds() > 9 ? this.getSeconds().toString() : '0' + this.getSeconds());
        str = str.replace(/s|S/g, this.getSeconds());

        return str;
    }


    return zq;
})($, zq);
