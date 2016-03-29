/*****************************************************************************
 *
 * @author 郑志强
 * 
 * @requires jQuery zq
 * 
 * @description  zq的对象的一些公用组件封装
 *
 *****************************************************************/
zq = (function (zq, $) {


    $.extend($.prototype, {
        htmlCode: function () {
            var $temp = $("<div/>");
            $temp.append(this);
            return $temp.html();
        }
    });


    return zq;

})(zq, $);

zq = (function (zq, $) {
    function _showLoading() { }
    function _closeLoading() { }
    /**
    * 全屏操作
    */
    zq.fullScreen = function (element) {
        var support = document.fullscreenEnabled || document.mozFullScreenEnabled || document.webkitFullscreenEnabled || document.msFullscreenEnabled;
        if (support) {
            document.exitFullscree ? document.exitFullscree() : document.mozCancelFullScreen ? document.mozCancelFullScreen() : document.webkitExitFullscreen ? document.webkitExitFullscreen() : alert('退出全屏失败');
            if (element.requestFullscreen) {
                element.requestFullscreen();
            } else if (element.mozRequestFullScreen) {
                element.mozRequestFullScreen();
            } else if (element.webkitRequestFullscreen) {
                element.webkitRequestFullscreen();
            } else if (element.msRequestFullscreen) {
                element.msRequestFullscreen();
            }
        }
    }
    /****
     * 公开的批量删除
     */
    zq.standardDel = function (options) {
        var opts = $.extend(true, {
            msg: "确定要删除?",
            checkedItems: [],
            url: "",
            success: function (result) { }
        }, options);

        if (!opts.url) {
            parent.layer.alert("请填写请求URL!");
            return;
        }

        if (opts.checkedItems.length === 0) {
            parent.layer.alert("至少选中一项!");
            return;
        }

        parent.layer.confirm(opts.msg, function (index) {
            var ids = [];
            $.each(opts.checkedItems, function (i, n) {
                ids.push(n.value);
            });
            $.post(opts.url, { ids: opts.checkedItems.join(",") }, opts.success);
            parent.layer.close(index);
        });
    }
    /****
    * 公开的表单提交
    */
    zq.submitForm = function (options) {
        var opts = $.extend(true, {
            url: "",
            $form: $("form:first"),
            success: function (result) { },//自定义成功
            error: function (result) { alert("发现系统错误 <BR/>错误码：" + result.status + "<BR/>"); }
        }, options);
        var url = opts.url || opts.$form.attr("action");
        if (!url) {
            alert("未设置表单提交地址!");
            return;
        }
        if (opts.$form.valid()) {
            var ajaxopts = {
                url: url,
                dataType: "json",
                error: opts.error,
                beforeSubmit: function (formData, jqForm) {
                    //针对复选框和单选框 处理
                    $(":checkbox,:radio", jqForm).each(function () {
                        if (!existInFormData(formData, this.name)) {
                            formData.push({ name: this.name, type: this.type, value: this.checked });
                        }
                    });
                    try {
                        for (var i = 0, l = formData.length; i < l; i++) {
                            var o = formData[i];
                            if (o.type === "checkbox" || o.type === "radio") {
                                if (o.value === "on") {
                                    o.value = $("input[name='" + o.name + "']", jqForm)[0].checked ? "true" : "false";
                                }
                            }
                        }
                    } catch (e) {
                        alert(e.message);
                    }
                },
                beforeSend: function (a, b, c) { _showLoading(); },
                complete: function () { _closeLoading(); },
                success: opts.success
            };
            opts.$form.ajaxSubmit(ajaxopts);
        }

        function existInFormData(formData, name) {
            for (var i = 0, l = formData.length; i < l; i++) {
                var o = formData[i];
                if (o.name === name) return true;
            }
            return false;
        }
    }
    /****
    * 公开的异步提交
    */
    zq.post = function (options) {
        var opts = $.extend(true, {
            dataType: "json",
            url: "",
            data: {},
            success: function (result) { },//自定义成功
            error: function (result) { alert("发现系统错误 <BR/>错误码：" + result.status + "<BR/>"); }
        }, options);

        if (!opts.url) alert("请设置请求地址!");

        var ajaxopts = {
            data: opts.data,
            url: opts.url,
            dataType: opts.dataType,
            type: "POST",
            beforeSubmit: function () { },
            beforeSend: function (a, b, c) { _showLoading(); },
            complete: function () { _closeLoading(); },
            success: opts.success
        };
        $.ajax(ajaxopts);
    }
    /****
    * 表格
    */
    zq.table = function (options) {
        /** 
         * 表格多语言设置
         */
        var language = {
            "sProcessing": "处理中...",
            "sLengthMenu": "_MENU_ 记录/页",
            "sZeroRecords": "没有匹配的记录",
            "sInfo": "显示第 _START_ 至 _END_ 项记录，共 _TOTAL_ 项",
            "sInfoEmpty": "显示第 0 至 0 项记录，共 0 项",
            "sInfoFiltered": "(由 _MAX_ 项记录过滤)",
            "sInfoPostFix": "",
            "sSearch": "过滤:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "首页",
                "sPrevious": "上页",
                "sNext": "下页",
                "sLast": "末页"
            }
        };
        /** 
         * 表格列默认设置
         */
        var columnDefs = [
            {
                sDefaultContent: "" //列默认值为""，以防数据中没有此值，DataTables加载数据的时候报错
               , aTargets: ["_all"] //设置所有列
               , "searchable": false
            }
        ];
        var opts = $.extend(true, {
            $searchBtn: null,
            $table: null,
            $searchForm: null,
            paging: true,
            columns: [],
            rowClickEvent: function () { },
            initComplete: function () { },
            footerCallback: function (tfoot, data, start, end, display) { },
            drawCallback: function (settings) { },
            ajaxsetting: {
                url: "", //设置异步请求数据接口
                type: "POST",
                cache: false,
                data: function (d) { d.filter = JSON.stringify(opts.$searchForm.serializeObject()); }
            }
        }, options);
        /** 
         * 表格
         */
        var table = opts.$table.DataTable({
            //"dom": '<"top"i>rt<"bottom"flp><"clear">'
            "oLanguage": language
            , ordering: false
            , "columns": opts.columns
            , "columnDefs": columnDefs
            , "serverSide": true//开启服务器模式
            , "processing": true//显示加载信息.
            //, "scrollY": "200px"//垂直滚动条
            , "paging": opts.paging//允许表格分页
            , "pagingType": "full_numbers"//分页类型
            , "ajax": opts.ajaxsetting
            , "bFilter": false
            , autoWidth: true
            , initComplete: opts.initComplete
            , "footerCallback": opts.footerCallback
            , "drawCallback": opts.drawCallback
        });
        opts.$table.on("click", "tbody tr td:not(.td-debar)", opts.rowClickEvent);
        /** 
        * 查询
        */
        opts.$searchBtn.on("click", function () { table.draw(true); });



        return table;
    }

    return zq;

})(zq, $);



