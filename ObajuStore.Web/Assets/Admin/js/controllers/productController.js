
var product = {
    init: function () {
        product.registerEvents();
        product.loadImages();
    },
    registerEvents: function () {
        $("#uploadImage").off('click').on('click', function () {
            $('#imageModal').modal('show');
            product.loadImages();
        });

        $("#btnSubmit").off('click').on('click', function () {
            product.setTextBoxValue();
            var images = [];

            $('#imageModal').modal('hide');
        });

        $("#btnPickImages").off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                var html = '<div class="float-left"><img src=' + url + ' title="Nhấp 2 lần để xóa"' +
                   ' class="margin-r-5 margin-bottom btn-removeImage" width="100"  />' +
                   '<input type="hidden" class="hidImage" value=' + url + '></div>';
                $('.imageList').append(html);
                $('.btn-removeImage').dblclick(function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                    product.imagesValue();

                });
            };

            finder.popup();
        });

    },
    imagesValue: function () {
        var images = [];
        $.each($('.imageList .hidImage'), function (i, item) {
            images.push($(item).val());
        });
        $('#imagesCounter').text(images.length + ' ảnh được chọn');
        return images;
    }, loadImages: function () {
        $.ajax({
            url: '/AdminObajuStore/Product/LoadImages',
            type: 'GET',
            data: {
                id: $('#hidProductID').val()
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                $('#imagesCounter').text(data.length + ' ảnh được chọn');
                var html = '';
                $.each(data, function (i, item) {
                    html += '<div class="float-left"><img src=' + item + ' title="Nhấp 2 lần để xóa"' +
                  ' class="margin-r-5 margin-bottom btn-removeImage" width="100"/>' +
                  '<input type="hidden" class="hidImage" value=' + item + '></div>';
                });
                $('.imageList').html(html);
                product.setTextBoxValue();

                $('.btn-removeImage').dblclick(function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                    product.imagesValue();
                });

            }
        });
    },
    setTextBoxValue: function () {
        $('#txtMoreImages').val(JSON.stringify(product.imagesValue()));
    }
}
product.init();

