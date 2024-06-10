

var chatHub = $.connection.chatHub;

var chatController = {
    init: function () {
        chatController.LoadData();
        chatController.RegesterEvent();
    },

    RegesterEvent: function () {


        $.connection.hub.start().done(function () {
            $('#send-button').off('click').on('click', function () {
                var nguoi_gui = $('#txtNguoiGui').val();
                var nguoi_nhan = $('#txtNguoiNhan').val();
                var message = $('#message-input').val();
                chatController.sendMessage(nguoi_gui, nguoi_nhan, message);
            });
        });

    },

    sendMessage: function (nguoi_gui, nguoi_nhan, message) {
        chatHub.server.send(nguoi_gui, nguoi_nhan, message);
    },

    LoadData: function () {
        $.ajax({
            type: 'Get',
            datatype: 'json',
            url: '/MessageWebSockets/LoadData',
            success: function (has) {
                if (has.success == true) {
                    var data = has.data;
                    var txtNguoiGui = has.txtNguoiGui;
                    var html = '';
                    var template = $('#data-template-User').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            UsersId: item.UsersId,
                            IdDetailUser: item.IdDetailUser,
                            NameImage: item.NameImage,
                            NameUser: item.NameUser,
                        });
                    });
                    $('#lstData').html(html);
                    $('#txtNguoiGui').val(txtNguoiGui)
                    chatController.RegesterEvent();
                } else {
                    toastr["warning"](has.message);
                }
            }
        })
    },

    ChatRevice: function (IdDetailUser) {
        $('#txtNguoiNhan').val(IdDetailUser)

        $.ajax({
            type: 'Get',
            datatype: 'json',
            url: '/MessageWebSockets/LoadMessage',
            data: {
                nguoi_gui: $('#txtNguoiGui').val(),
                nguoi_nhan: $('#txtNguoiNhan').val(),
            },
            success: function (has) {
                if (has.success == true) {
                    var check = has.check;
                    var Chat = has.Chat;
                    var html = '';
                    var template1 = '\n<div class="row m-b-20 send-chat">\n<div class="col">\n<div class="msg">\n<p class="m-b-0">{{Content}}</p>\n</div>\n</div>\n</div>\n';
                    var template2 = '\n<div class="row m-b-20 received-chat">\n<div class="col">\n<div class="msg">\n<p class="m-b-0">{{Content}}</p>\n</div>\n</div>\n</div>\n'
                    $.each(Chat, function (i, item) {
                        if (check == item.SenderId) {
                            html += Mustache.render(template1, {
                                Content: item.Content,
                            });
                        } else if (check == item.ReceiverId) {
                            html += Mustache.render(template2, {
                                Content: item.Content,
                            });
                        } else {
                            html = "";
                        }

                    });
                    $('#message-list').html(html);
                    chatController.RegesterEvent();
                } else {
                    toastr["warning"](has.message);
                }
            }
        })
    },

}

chatController.init();


chatHub.client.receiveMessage = function (nguoi_gui, nguoi_nhan) {
    if (($('#txtNguoiGui').val() == nguoi_gui && $('#txtNguoiNhan').val() == nguoi_nhan) || ($('#txtNguoiGui').val() == nguoi_nhan && $('#txtNguoiNhan').val() == nguoi_gui)) {
        $.ajax({
            type: 'Get',
            datatype: 'json',
            url: '/MessageWebSockets/LoadMessage',
            data: {
                nguoi_gui: nguoi_gui,
                nguoi_nhan: nguoi_nhan,
            },
            success: function (has) {
                if (has.success == true) {
                    var check = has.check;
                    var Chat = has.Chat;
                    var html = '';
                    var template1 = '\n<div class="row m-b-20 send-chat">\n<div class="col">\n<div class="msg">\n<p class="m-b-0">{{Content}}</p>\n</div>\n</div>\n</div>\n';
                    var template2 = '\n<div class="row m-b-20 received-chat">\n<div class="col">\n<div class="msg">\n<p class="m-b-0">{{Content}}</p>\n</div>\n</div>\n</div>\n'
                    $.each(Chat, function (i, item) {
                        if (check == item.SenderId) {
                            html += Mustache.render(template1, {
                                Content: item.Content,
                            });
                        } else if (check == item.ReceiverId) {
                            html += Mustache.render(template2, {
                                Content: item.Content,
                            });
                        } else {
                            html = "";
                        }

                    });
                    $('#message-list').html(html);
                    // Gọi hàm receiveMessage trên client với connectionId của người nhận
                    // Cuộn xuống cuối
                    var chatContainer = document.querySelector('.cust-scroll-chat');
                    chatContainer.scrollTop = chatContainer.scrollHeight;

                    // Cập nhật thanh cuộn
                    px2.update();
                    chatController.RegesterEvent();
                } else {
                    toastr["warning"](has.message);
                }
            }
        })
    }
};

