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

    zq.cookie = {
        add: function (name, value, options) {
            if (typeof value != "undefined") { // name and value given, set cookie
                options = options || {};
                if (value === null) {
                    value = "";
                    options.expires = -1;
                }
                var expires = "";
                if (options.expires && (typeof options.expires == "number" || options.expires.toUTCString)) {
                    var date;
                    if (typeof options.expires == "number") {
                        date = new Date();
                        date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
                    } else {
                        date = options.expires;
                    }
                    expires = "; expires=" + date.toUTCString(); // use expires attribute, max-age is not supported by IE
                }
                var path = options.path ? "; path=" + options.path : "";
                var domain = options.domain ? "; domain=" + options.domain : "";
                var secure = options.secure ? "; secure" : "";
                return (document.cookie = [
                    name, "=", encodeURIComponent(value),
                    expires,
                    path,
                    domain,
                    secure
                ].join(""));
            }
        },
        get: function (name) {
            var cookieValue = null;
            if (document.cookie && document.cookie !== "") {
                var cookies = document.cookie.split(";");
                for (var i = 0; i < cookies.length; i++) {
                    var cookie = jQuery.trim(cookies[i]);
                    // Does this cookie string begin with the name we want?
                    if (cookie.substring(0, name.length + 1) === (name + "=")) {
                        cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                        break;
                    }
                }
            }
            return cookieValue;
        }
    }

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
        if (arguments.length === 0)
            return null;
        var str =this;
        for (var i = 0; i < arguments.length; i++) {
            var re = new RegExp("\\{" + i + "\\}", "gm");
            str = str.replace(re, arguments[i]);
        }
        return str;
    };

    Date.prototype.Format = function (formatStr) {
        var str = formatStr;
        var week = ["日", "一", "二", "三", "四", "五", "六"];

        str = str.replace(/yyyy|YYYY/, this.getFullYear());
        str = str.replace(/yy|YY/, (this.getYear() % 100) > 9 ? (this.getYear() % 100).toString() : "0" + (this.getYear() % 100));

        str = str.replace(/MM/, this.getMonth() > 9 ? this.getMonth().toString() : "0" + this.getMonth());
        str = str.replace(/M/g, this.getMonth());

        str = str.replace(/w|W/g, week[this.getDay()]);

        str = str.replace(/dd|DD/, this.getDate() > 9 ? this.getDate().toString() : "0" + this.getDate());
        str = str.replace(/d|D/g, this.getDate());

        str = str.replace(/hh|HH/, this.getHours() > 9 ? this.getHours().toString() : "0" + this.getHours());
        str = str.replace(/h|H/g, this.getHours());
        str = str.replace(/mm/, this.getMinutes() > 9 ? this.getMinutes().toString() : "0" + this.getMinutes());
        str = str.replace(/m/g, this.getMinutes());

        str = str.replace(/ss|SS/, this.getSeconds() > 9 ? this.getSeconds().toString() : "0" + this.getSeconds());
        str = str.replace(/s|S/g, this.getSeconds());

        return str;
    }
    /** 
     * 获取指定日期对象当前表示的季度（0 - 3）
     * 注:私有不对外公开!
     */
    var _getQuarter = function (date) {
        date = new Date(date);
        var m = date.getMonth();
        var q = 0;
        if (m >= 0 && m < 3) {
            q = 0;
        } else if (m >= 3 && m < 6) {
            q = 1;
        } else if (m >= 6 && m < 9) {
            q = 2;
        } else if (m >= 9 && m < 12) {
            q = 3;
        }
        return q;
    };

    /**
    * 扩展String类型的formatString功能
    * 
    * @example '字符串{0}字符串{1}字符串'.format('第一个变量','第二个变量');
    * @returns 格式化后的字符串
    */
    String.prototype.format = function () {
        var thisString = this;
        for (var i = 0; i < arguments.length; i++) {
            thisString = thisString.replace("{" + i + "}", arguments[i]);
        }
        return thisString;
    }

    /**
    * 获取当前日期时间的长字符串格式，返回的日期时间字符串格式如 “2013年05月16日 星期四 夏季, 下午 15:38:11”
    *  
    */
    Date.prototype.toLongDateTimeString = function () {
        var date = this;
        var year = date.getFullYear(),
            month = date.getMonth(),
            day = date.getDate(),
            hours = date.getHours(),
            minutes = date.getMinutes(),
            seconds = date.getSeconds(),
            week = date.getDay(),
            quarter = _getQuarter(date),
            hoursArray = ["午夜", "凌晨", "早上", "上午", "中午", "下午", "傍晚", "晚上"],
            weekArray = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
            //monthArray = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"],
            quarterArray = ["春", "夏", "秋", "冬"],
            hourSpan = hoursArray[Math.floor(parseFloat(hours) / 3)],
            weekSpan = weekArray[week],
            //monthSpan = monthArray[month],
            quarterSpan = quarterArray[quarter];
        return "{0}年{1}月{2}日 {3} {4}季, {5} {6}:{7}:{8}".format(
            year,
            ("" + (month + 101)).substr(1),
            ("" + (day + 100)).substr(1),
            weekSpan,
            quarterSpan,
            hourSpan,
            ("" + (hours + 100)).substr(1),
            ("" + (minutes + 100)).substr(1),
            ("" + (seconds + 100)).substr(1));
    };

    return zq;
})($, zq);
