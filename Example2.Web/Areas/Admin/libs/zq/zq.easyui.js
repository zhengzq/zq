/*****************************************************************************
 *
 * @author 郑志强
 * 
 * @requires jQuery zq EasyUI
 * 
 * @description 对Easyui的初始化和扩展
 *
 *****************************************************************/

var zq = (function (zq) {
    /**
     * 更改easyui加载panel时的提示文字  
     */
    $.extend($.fn.panel.defaults, {
        loadingMessage: "加载中....",
        onBeforeLoad: function (param) {
            zq.topJQuery.messager.progress({
                text: "数据加载中...."
            });
        },
        onOpen: function (data) {
            zq.topJQuery.messager.progress('close');
        }
    });
    /**
     * 更改easyui加载grid时的提示文字     
     */
    $.extend($.fn.datagrid.defaults, {
        loadMsg: "数据加载中...."
    });
    /**
     * panel关闭时回收内存，主要用于layout使用iframe嵌入网页时的内存泄漏问题
     */
    $.extend($.fn.panel.defaults, {
        onBeforeDestroy: function () {
            var frame = $("iframe", this);
            try {
                if (frame.length > 0) {
                    for (var i = 0; i < frame.length; i++) {
                        frame[i].src = "";
                        frame[i].contentWindow.document.write("");
                        frame[i].contentWindow.close();
                    }
                    frame.remove();
                    if (navigator.userAgent.indexOf("MSIE") > 0) {// IE特有回收内存方法
                        try {
                            CollectGarbage();
                        } catch (e) {
                        }
                    }
                }
            } catch (e) {
            }
        }
    });

    /**
     * 
     * 通用错误提示
     * 
     * 用于datagrid/treegrid/tree/combogrid/combobox/form加载数据出错时的操作
     */
    zq.onLoadError = {
        onLoadError: function (XMLHttpRequest) {
            if (parent.$ && parent.$.messager) {
                parent.$.messager.progress("close");
                $.messager.show({
                    title: "错误",
                    msg: XMLHttpRequest.responseText,
                    showType: "show",
                    timeout: 0,
                    width: 400,
                    height: 400
                });
            } else {
                $.messager.progress("close");
                $.messager.alert("错误", XMLHttpRequest.responseText);
            }
        }
    };
    $.extend($.fn.datagrid.defaults, zq.onLoadError);
    $.extend($.fn.treegrid.defaults, zq.onLoadError);
    $.extend($.fn.tree.defaults, zq.onLoadError);
    $.extend($.fn.combogrid.defaults, zq.onLoadError);
    $.extend($.fn.combobox.defaults, zq.onLoadError);
    $.extend($.fn.form.defaults, zq.onLoadError);

    var _creatShowIcon = function (icon, msg) {
        return "<div class=\"messager-icon messager-{0}\"></div><div>{1}</div><div style=\"clear:both;\"/>".format(icon, msg);
    }

    zq.show = {
        defaults: {
            title: "提示信息",
            msg: "请设置提示信息内容",
            showType: "slide",
            icon: ""
        },
        showTopCenter: function (options) {
            var opts = $.extend({
                style: {
                    right: "",
                    top: document.body.scrollTop + document.documentElement.scrollTop,
                    bottom: ""
                }
            }, zq.show.defaults, options);

            opts.msg = _creatShowIcon(opts.icon, opts.msg);

            zq.topJQuery.messager.show(opts);
        },
        showBottomRight: function (options) {
            var opts = $.extend({}, zq.show.defaults, options);

            opts.msg = _creatShowIcon(opts.icon, opts.msg);

            zq.topJQuery.messager.show(opts);
        }
    };

    zq.easyui = {
        option: {
            tab: null
        },
        /**
         * 创建一个模式化的dialog               
         */
        modalDialog: function (options) {
            if (!options || !options.url) {
                zq.show.showTopCenter({ title: "显示失败", msg: "未设置dialog!" }); return;
            }
            var usedJQuery = options.topMost ? $ : zq.topJQuery,
             $dialogDiv = usedJQuery("<div/>");

            var opts = $.extend(false, {
                title: "&nbsp;",
                width: 640,
                height: 480,
                modal: true,
                onClose: function () { $dialogDiv.dialog("destroy"); }
            }, options);
            if (options.buttons && typeof options.buttons === "object") {
                opts.buttons = [];
                $.each(options.buttons, function (i, n) {
                    opts.buttons.push(n);
                });
                opts.buttons.push({
                    text: "关闭",
                    iconCls: "icon-cancel",
                    handler: function (dia) { $dialogDiv.dialog('destroy'); }
                });
            }

            opts.modal = true;// 强制此dialog为模式化，无视传递过来的modal参数
            if (options.url) {
                var $iframe = $("<iframe/>").attr({
                    src: options.url,
                    allowTransparency: true,
                    scrolling: "auto",
                    width: "100%",
                    height: "99%",
                    frameBorder: 0
                });

                opts.content = $iframe.htmlCode();
            }

            return $dialogDiv.dialog(opts);
        },
        /**
         * 更换主题(需要设置模板的样式LINK的ID)               
         * 
         * @param themeName
         */
        changeTheme: function (themeName, setCookie) {
            if (!themeName) themeName = parent.zq.cookie.get("easyuiTheme");
            if (!themeName) themeName = "default";
            var $easyuiTheme = $("#easyuiTheme");
            var url = $easyuiTheme.attr("href");
            var href = url.substring(0, url.indexOf("themes")) + "themes/" + themeName + "/easyui.css";
            $easyuiTheme.attr("href", href);

            var $iframe = $("iframe");
            if ($iframe.length > 0) {
                for (var i = 0; i < $iframe.length; i++) {
                    var ifr = $iframe[i];
                    try {
                        $(ifr).contents().find("#easyuiTheme").attr("href", href);
                    } catch (e) {
                        try {
                            ifr.contentWindow.document.getElementById("easyuiTheme").href = href;
                        } catch (e) {
                        }
                    }
                }
            }
            if (setCookie) {
                zq.cookie.add("easyuiTheme", themeName, { expires: 7 });
            }
        },
        /**
        *显示tab页面(iframe模式)
        *
        *@param title 标题
        *@param url 链接
        *@param iconCls 图标样式
        */
        tabShow: function (title, url, iconCls) {
            if (!url) return;
            var _t = zq.easyui.option.tab;
            if (_t) {
                if (_t.tabs("exists", title)) {
                    _t.tabs("select", title);
                } else {
                    var $iframe = $("<iframe/>").attr({
                        src: url,
                        allowTransparency: true,
                        scrolling: "auto",
                        width: "100%",
                        height: "99%",
                        frameBorder: 0
                    });

                    _t.tabs("add", {
                        title: title,
                        content: $iframe.htmlCode(),
                        border: false,
                        iconCls: iconCls,
                        closable: true,
                        fit: true
                    });
                }
            } else {
                alert("请在首页设置tabs对象!");
            }
        },
        iframePanel: function (url, $target) {
            if (typeof $target !== "object") alert("error");
            var $iframe = $("<iframe/>").attr({
                src: url,
                allowTransparency: true,
                scrolling: "auto",
                width: "100%",
                height: "99%",
                frameBorder: 0
            });


            //$('#p').panel('refresh', '/Order/List');
            $target.panel({
                content: $iframe.htmlCode(),
                fit: true
            });
        },
        /**
        * 初始化toolbar
        *
        * @param toolbarItems 控制器名
        * @param toobarBtnItemExcuter 请求地址
        */
        toolbarInit: function (tools, toobarBtnItemExcuter) {
            var toobar = [];

            $.each(tools, function (i, n) {
                if (n.btnType === "tool") {
                    var tool = {
                        iconCls: data.iconCls,
                        handler: function () { toobarBtnItemExcuter(n.action); }
                    };
                    if (n.text) tool.text = n.text;
                    toolbar.push(tool);
                }
            });
            return toolbar;
        },
        /**
        * 获取dialog第一个Ifram
        *
        * @param dialog 指定的弹框
        * @param options 其他参数。
        */
        getIframe: function (dialog, options) {
            return dialog.find("iframe").get(0).contentWindow;
        }
    };


    return zq;
})(zq)
