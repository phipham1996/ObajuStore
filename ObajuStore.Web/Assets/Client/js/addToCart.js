$('#add-to-cart').off('click').on('click', function (e) {
    e.preventDefault();
    var productId = parseInt($(this).data('id'));
    addToCart(productId);
});


function addToCart(productId) {
    $.ajax({
        url: "/ShoppingCart/Add",
        type: "post",
        dataType: "json",
        data: {
            productId: productId
        },
        success: function (res) {
            if (!res.status)
                toastr.warning('Sản phẩm đang hết hàng! Vui lòng quay lại sau.');
            else
                toastr.success('Thêm vào giỏ thành công.');
        }
    })
}