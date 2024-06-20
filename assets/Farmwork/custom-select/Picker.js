$(document).ready(function () {
    $('.picker').each(function () {
        var $select = $(this);
        var $container = $('<div class="picker-custom"></div>');
        var $trigger = $('<span class="pc-trigger">' + ($select.find('option:selected').text() || 'Chọn') + '</span>');
        var $arrow = $('<span class="pc-arrow">&#9662;</span>'); // Thêm mũi tên chỉ xuống
        var $list = $('<div class="pc-list"><ul></ul></div>');

      

        // Thêm các phần tử vào container
        $container.append($trigger).append($arrow).append($list);
        $select.after($container);

        // Xử lý khi nhấp vào trigger
        $container.on('click', function () {
            $list.toggle();
        });

        // Xây dựng danh sách các mục
        $select.find('option').each(function () {
            var $option = $(this);
            var $item = $('<li data-value="' + $option.val() + '">' + $option.text() + '</li>');
            $list.find('ul').append($item);

            // Xử lý khi nhấp vào một mục
            $item.on('click', function () {
                $select.val($option.val()).trigger('change');
                $trigger.text($option.text());
                $list.hide(); // Ẩn toàn bộ hộp chọn tùy chỉnh sau khi chọn
            });
        });
   
        // Ẩn danh sách khi nhấp bên ngoài
        $(document).mouseup(function (e) {
            if (!$container.is(e.target) && $container.has(e.target).length === 0) {
                $list.hide();
            }
        });

        // Ẩn select gốc
        $select.hide();
    });
 
});
