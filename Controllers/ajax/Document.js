var files;
var zipBlob;
var myMenuImageDocument = [
    {
        icon: 'fas fa-long-arrow-alt-down text-primary',
        label: 'Tải file',
        action: function (option, contextMenuIndex, optionIndex) {
        },
        submenu: null,
        disabled: false
    },
    {
        icon: 'fa fa-edit text-info',
        label: 'Đổi tên file',
        action: function (option, contextMenuIndex, optionIndex) {

        },
        submenu: null,
        disabled: false
    },
    {
        icon: 'fa fa-trash text-danger',
        label: 'Xóa file',
        action: function (option, contextMenuIndex, optionIndex) {

        },
        submenu: null,
        disabled: false
    },
];

var myController = {
    init: function () {
        myController.SetFileinputLocales();
        myController.RegesterEvent();
        myController.LoadFile();
        $("#modal-file").modal('show');
        $("#checkAll").prop('checked', false);
    },

    RegesterEvent: function () {

        $("#btnUpload").off('click').on('click', function () {
            myController.SaveFile();
        });

        $("#btnFileDinhKem").off('click').on('click', function () {
            $("#dowloadZip").hide();
            $("#btnDowloadFile").show();
            myController.LoadFile();
            $("#modal-file").modal('show');
            $("#checkAll").prop('checked', false);
        });

        $("#btnRemoveFile").off('click').on('click', function () {
            myController.RemoveFile();
        });

        $("#checkAll").off('click').on('click', function () {
            myController.checkAll();
        });

        $("#btnDowloadFile").off('click').on('click', function () {
            myController.DowloadFile();
        });

        $('img[data-action="ContextMenuEdit"]').off('contextmenu').on('contextmenu', function (e) {
            superCm.createMenu(myMenuImageDocument, e);
            $("#LockScreenFull").addClass('modal-backdrop fade show');
            $("#LockScreenFull").css('z-index',99999999);
            return false;
        });

        $('a[data-gallery="mixedgallery"]').off('click').on('click', function (e) {
            $("#LockScreenFull").addClass('modal-backdrop fade show');
            $("#LockScreenFull").css('z-index', 999999999999);
        });

        $("#dowloadZip").off('click').on('click', function () {
            $("#dowloadZip").hide();
            $("#btnDowloadFile").show();
            window.open("/thu-muc-luu-tru", '_self');
            $('.allCK').prop('checked', false).iCheck('update');
        });

        $('#duong_dan_tai_lieu').on('change', function (e) {
            files = e.target.files;
            if (files.length > 0) {
                for (var x = 0; x < files.length; x++) {
                    if (files[x].size >= 52428800) {
                        XNoti_CanhBao('Chỉ được upload file dưới 50mb', 'Thông báo');
                        $("#duong_dan_tai_lieu").filestyle('clear');
                        $("#duong_dan_tai_lieu").filestyle('placeholder', "Không có tệp đính kèm nào");
                        $('#is_thay_doi').val('true');
                        return;
                    }
                    $('#is_thay_doi').val('true');
                }
            }
        });
    },

    checkAll: function () {
        var check = $("#checkAll").prop('checked');
        if (check === true) {
            $('.allCK').prop('checked', true).iCheck('update');
        }
        else {
            $('.allCK').prop('checked', false).iCheck('update');
        }
    },



    SetFileinputLocales: function () {
        $('#duong_dan_tai_lieu').fileinputLocales.en.removeLabel = ''
        $('#duong_dan_tai_lieu').fileinputLocales.en.removeIcon = '<i class="feather icon-trash"></i>'
        $('#duong_dan_tai_lieu').fileinputLocales.en.removeClass = 'btn btn-grd-danger'
        $('#duong_dan_tai_lieu').fileinputLocales.en.browseLabel = ''
        $('#duong_dan_tai_lieu').fileinputLocales.en.browseIcon = '<i class="feather icon-file"></i>'
        $('#duong_dan_tai_lieu').fileinputLocales.en.browseClass = 'btn btn-grd-info'
    },

    SaveFile: function () {
        if (files != undefined && files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    if (files[x].size >= 52428800) {
                        XNoti_CanhBao('Chỉ được upload file dưới 50mb', 'Thông báo');
                        $('#duong_dan_tai_lieu').val("");
                        return;
                    }
                    data.append(files[x].name, files[x]);
                }
                $.ajax({
                    type: "POST",
                    url: '/ThuMucLuuTru/SaveFile',
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (response) {
                        if (response.status == true) {
                            toastr["success"]('Cập nhập thành công');
                            $("#duong_dan_tai_lieu").fileinput('clear').trigger('custom-event');
                            myController.LoadFile();
                            $("#modal-file").modal('show');
                        } else {
                            toastr["warning"](response.message);
                        }
                    },
                    error: function (response) {
                        toastr["warning"](response.message);
                    }
                });
            } else {
                toastr["warning"]("Upload file thất bại!");
            }
        } else {
            toastr["warning"]("Bạn chưa chọn file!");
        }
    },

    LoadFile: function () {
        ShowImageLoadDing();
        $.ajax({
            type: 'Get',
            datatype: 'json',
            url: '/ThuMucLuuTru/LoadFile',
            success: function (has) {
                if (has.status == true) {
                    var data = has.data;
                    var html = '';
                    var template = $('#data-template-Image').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            url: item.UrlDocument,
                            IdDocument: item.IdDocument,
                            NameDocument: item.NameDocument,
                        });
                    });
                    $('#lstImage').html(html);
                    $('#CountList').html(has.CountList);
                    myController.RegesterEvent();
                    HideImageLoadDing();
                } else {
                    toastr["warning"](has.message);
                }
            }
        })
    },

    RemoveFile: function () {
        var lstCheckedItem = "";
        $(".allCK").each(function (e, data) {
            if (data.checked == true) {
                var id = data.id;
                if (id > 0) {
                    lstCheckedItem += id + ",";
                }
            }
        });
        if (lstCheckedItem == "") {
            toastr["warning"]("Bạn chưa chọn file cần xóa!");
            return;
        }
        bootbox.confirm({
            message: '<div class="alert alert-warning col-lg-12 f-16" role="alert">Bạn có muốn xóa không? <font class="text-danger font-bold">Lưu ý : mọi quyết định sẽ được lưu lại và bạn không thể thay đổi</font></div>',
            closeButton: null,
            title: "Xác nhận biểu mẫu",
            buttons: {
                confirm: {
                    label: '<i class="feather icon-check"></i> Tôi đồng ý',
                    className: 'btn-sm btn-round btn-grd-success'
                },
                cancel: {
                    label: '<i class="feather icon-loader"></i> Thoát',
                    className: 'btn-sm btn-round btn-grd-danger'
                }
            },
            callback: function (result) {
                if (result == true) {
                    $.ajax({
                        type: 'Post',
                        datatype: 'json',
                        data: {
                            lstCheckedItem: lstCheckedItem
                        },
                        url: '/ThuMucLuuTru/RemoveFile',
                        success: function (has) {
                            if (has.status == true) {
                                toastr["success"]("Xóa thành công!");
                                myController.LoadFile();
                            } else {
                                toastr["warning"](has.message);
                            }
                        }
                    })
                }
            }
        });
       

    },

    DowloadFile: function () {
        var lstCheckedItem = "";
        $(".allCK").each(function (e, data) {
            if (data.checked == true) {
                var id = data.id;
                if (id > 0) {
                    lstCheckedItem += id + ",";
                }
            }
        });
        if (lstCheckedItem == "") {
            toastr["warning"]("Bạn chưa chọn file cần tải!");
            return;
        }
        ShowImageLoadDing();
        $.ajax({
            type: 'Post', // Sử dụng phương thức 'Post'
            dataType: 'json',
            data: {
                lstCheckedItem: lstCheckedItem
            },
            url: '/ThuMucLuuTru/DowloadFile',
            success: function (data) {
                if (data.status == false) {
                    toastr["warning"](data.message);
                } else {
                    $("#btnDowloadFile").hide();
                    $("#dowloadZip").show();
                    $("#dowloadZip").attr("href", data.tempZipFilePath)
                }
                myController.RegesterEvent();
                HideImageLoadDing();
            }
        });
    },


};
myController.init();

