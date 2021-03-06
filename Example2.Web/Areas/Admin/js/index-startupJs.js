﻿$(function () {
    var $mainbody = $("#mainbody");
    var $toolbar = $("#toolbar");
    var $header = $("#header");
    var $btnShow = $("#btnShowNorth");
    zq.easyui.option.tab = $("#tab");

    /**
    * 顶部时间
    */
    var initTimerSpan = function () {
        var timerSpan = $("#timerSpan"), interval = function () { timerSpan.text(new Date().toLongDateTimeString()); };
        interval();
        window.setInterval(interval, 1000);
    }();
    var theme = ($.cookie("easyuiTheme") && JSON.parse($.cookie("easyuiTheme"))) || zq.easyui.themeData[0];
    /**
    * 模板选择
    */
    $("#themeSelector").combobox({
        width: 140,
        editable: false,
        data: zq.easyui.themeData,
        valueField: "value",
        textField: "cnName",
        value: theme.value,
        onSelect: function (record) { zq.easyui.changeTheme(record, true); },
        onLoadSuccess: function () { zq.easyui.changeTheme(theme, true); }
    });

    /**
    * 全屏事件
    */
    $('#btnFullScreen').click(function () { zq.fullScreen(document.documentElement); });
    /*
    * 退出事件
    */
    $("#btnExit").click(function () { window.location = "/Admin/Account/logout"; });
    /**
    * 隐藏顶部事件
    */
    $("#btnHideNorth").click(function () { $mainbody.layout("collapse", "north") });
    /**
    * 显示顶部
    */
    $btnShow.click(function () { $mainbody.layout("expand", "north"); });
    var north = $mainbody.layout("panel", "north");
    var panel = north.panel("panel");
    var northopts = north.panel("options");
    var onCollapse = northopts.onCollapse;
    var onExpand = northopts.onExpand;
    northopts.onCollapse = function () {
        if ($.isFunction(onCollapse)) { onCollapse.apply(this, arguments); }
        $btnShow.show();
        $toolbar.addClass("top-toolbar-topmost").insertBefore(panel);
    };
    northopts.onExpand = function () {
        if ($.isFunction(onExpand)) { onExpand.apply(this, arguments); }
        $btnShow.hide();
        $toolbar.removeClass("top-toolbar-topmost").insertAfter($header);
    };

    $mainbody.layout("panel", "center").panel({
        onResize: function (width, height) {
            setIframeHeight("centerIframe", $mainbody.layout("panel", "center").panel("options").height - 5);
            function setIframeHeight(iframe, height) {
                iframe.height = height;
            };
        }
    });
    var tabPages = $("#tab").tabs({ fit: true, border: false });
});


