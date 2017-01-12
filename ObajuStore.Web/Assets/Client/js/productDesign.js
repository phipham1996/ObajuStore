
var mat_truoc_canvas = document.getElementById("mat-truoc-canvas");
var mat_sau_canvas = document.getElementById("mat-sau-canvas");
var mat_ngang_trai_canvas = document.getElementById("mat-ngang-trai-canvas");
var mat_ngang_phai_canvas = document.getElementById("mat-ngang-phai-canvas");

InitialCanvas(500, 500);

var ctx_mat_truoc = mat_truoc_canvas.getContext("2d");
var ctx_mat_sau = mat_sau_canvas.getContext("2d");
var ctx_mat_ngang_trai = mat_ngang_trai_canvas.getContext("2d");
var ctx_mat_ngang_phai = mat_ngang_phai_canvas.getContext("2d");
var img1 = document.getElementById("mat-truoc");
var img2 = document.getElementById("mat-sau");
var img3 = document.getElementById("mat-ngang-trai");
var img4 = document.getElementById("mat-ngang-phai");

var text = document.getElementById("txtMyText");
$(window).on('load', function () {
    ctx_mat_truoc.drawImage(img1, 0, 0, 500, 500);
    ctx_mat_sau.drawImage(img2, 0, 0, 500, 500);
    ctx_mat_ngang_trai.drawImage(img3, 0, 0, 500, 500);
    ctx_mat_ngang_phai.drawImage(img4, 0, 0, 500, 500);
    setImage("mat-truoc");
})
$(document).ready(function () {
    $('#loading').addClass('hide');
})

if ($("#txtMyText").bind('keypress keyup', function (event) {
    drawText($("#txtMyText").val(), "orange", "green", ctx_mat_truoc);
}));

function InitialCanvas(width, height) {
    mat_truoc_canvas.width = width;
    mat_truoc_canvas.height = height;

    mat_sau_canvas.width = width;
    mat_sau_canvas.height = height;

    mat_ngang_trai_canvas.width = width;
    mat_ngang_trai_canvas.height = height;

    mat_ngang_phai_canvas.width = width;
    mat_ngang_phai_canvas.height = height;
}

function drawText(text, fill, stroke, ctx) {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.drawImage(img, 0, 0, 500, 500);
    ctx.font = "15px verdana";
    ctx.fillStyle = fill;
    ctx.strokeStyle = stroke;
    ctx.lineWidth = 1;
    ctx.fillText(text, 10, 120);
    ctx.strokeText(text, 10, 120);
}
function setImage(id) {
    switch (id) {
        case "mat-truoc":
            mat_truoc_canvas_hidden(false);
            break;
        case "mat-sau":
            mat_sau_canvas_hidden(false);
            break;
        case "mat-ngang-trai":
            mat_ngang_trai_canvas_hidden(false);
            break;
        case "mat-ngang-phai":
            mat_ngang_phai_canvas_hidden(false);
            break;
        default:
            mat_truoc_canvas_hidden(false);
            break;
    }
}

function mat_truoc_canvas_hidden(res) {
    mat_truoc_canvas.hidden = res;
    mat_sau_canvas.hidden = !res;
    mat_ngang_trai_canvas.hidden = !res;
    mat_ngang_phai_canvas.hidden = !res;
}
function mat_ngang_phai_canvas_hidden(res) {
    mat_truoc_canvas.hidden = !res;
    mat_sau_canvas.hidden = !res;
    mat_ngang_trai_canvas.hidden = !res;
    mat_ngang_phai_canvas.hidden = res;
}
function mat_sau_canvas_hidden(res) {
    mat_truoc_canvas.hidden = !res;
    mat_sau_canvas.hidden = res;
    mat_ngang_trai_canvas.hidden = !res;
    mat_ngang_phai_canvas.hidden = !res;
}
function mat_ngang_trai_canvas_hidden(res) {
    mat_truoc_canvas.hidden = !res;
    mat_sau_canvas.hidden = !res;
    mat_ngang_trai_canvas.hidden = res;
    mat_ngang_phai_canvas.hidden = !res;
}

$('#wearOnline').on('click', function (e) {
    e.preventDefault();
    var dataURL = mat_truoc_canvas.toDataURL("image/png");
    $.ajax({
        url: "/ProductDesign/WearOnline",
        type: "post",
        data: {
            imgUrl: dataURL
        },
        success: function (response) {
            location.href = "http://localhost:63221/thu-do-online.htm";
        },
        error: function (xhr, ajaxOptions, thrownError)
        {
            alert(xhr.responseText);
        }
    });
})