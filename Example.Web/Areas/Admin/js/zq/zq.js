/*****************************************************************************
 *
 * @author ֣־ǿ
 * 
 * @requires jQuery
 * 
 * @description ��Ŀjs�ĺ��Ķ���
 *
 *****************************************************************/

var zq = (function ($, window) {

    var zq = function ($, window) { };
    /**
     * ����Ļ�������
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
 * zq�Ķ����һЩ��������
 * 
 * @author ֣־ǿ
 *
 * @requires  zq 
 */
zq = (function (zq) {
    /**
     * ���������ļ�
     * 
     * @params url  ��������
     * @params data ���ݵĲ�������
     * @params method ����ķ�ʽ(Ĭ��ֵΪpost����)
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
            return value === true ? "��" : "��";
        },
        /**
         * ��ʽ��ʱ��
         * 
         * @param str �账��ʱ���ַ���
         * @comment �߼��Ǵ����T��ʱ���ַ�����ʽ
         * @returns ��ʽ����ʱ���ַ���
         */
        timespFormater: function (value) {
            var day = (value / (24 * 60 * 60)).toFixed(0).toString();
            var hour = ((value - day * (24 * 60 * 60)) / (60 * 60)).toFixed(0).toString();
            var second = ((value - day * (24 * 60 * 60) - hour * (60 * 60)) / 60).toFixed(0).toString();
            return day + "��" + ("00" + hour).substr(hour.length, 2) + "ʱ" + ("00" + second).substr(second.length, 2) + "��";
        },
        /**
         * ��ʽ��ʱ����
         * 
         * @param value �账��ʱ���ַ���
         * @comment �߼��Ǵ����T��ʱ���ַ�����ʽ
         * @returns ��ʽ����ʱ���ַ���  [example:2010��12��23�� 10:53]
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
 * zq�Ķ����JS����չ
 * 
 * @author ֣־ǿ
 *
 * @requires  zq 
 */
zq = (function ($, zq) {
    /**
     * ���л���չ
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
        var week = ['��', 'һ', '��', '��', '��', '��', '��'];

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
