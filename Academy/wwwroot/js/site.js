//dateStr
$('.date-input').pDatepicker({ 
    format: 'YYYY/MM/DD',
    autoClose: true
});
var date = $("#lastDate").val();
if (date !== '' && date !== null && date !== undefined) {
    $('.date-input').val(date);
} else {
    $('.date-input').val('');
}

//toDateStr
$('.to-date-input').pDatepicker({ 
    format: 'YYYY/MM/DD',
    autoClose: true
});
var date = $("#lastToDate").val();
if (date !== '' && date !== null && date !== undefined) {
    $('.to-date-input').val(date);
} else {
    $('.to-date-input').val('');
}

/////


$('.date-input1').pDatepicker({ 
    format: 'YYYY/MM/DD',
    autoClose: true
});
var date = $("#lastDate1").val();
if (date !== '' && date !== null && date !== undefined) {
    $('.date-input1').val(date);
} else {
    $('.date-input1').val('');
}

$('.to-date-input1').pDatepicker({ 
    format: 'YYYY/MM/DD',
    autoClose: true
});
var date = $("#lastToDate1").val();
if (date !== '' && date !== null && date !== undefined) {
    $('.to-date-input1').val(date);
} else {
    $('.to-date-input1').val('');
}



function CheckPayType() {
    if ($("#payType").val() === "1") {
        $("#PerMonthPay").css("display", "block");
        $("#discountPay").css("display", "none");
        PayPerMonth();
    } else {
        $("#PerMonthPay").css("display", "none");
        $("#discountPay").css("display", "block");
        $("#payPerMonth").css("display", "none");
    }
}

function PayPerMonth(priceVal = 0) {
    var price = priceVal === 0 ? parseInt($("#AppClass_Price").val()) : priceVal;
    var count = parseInt($("#PerMonthDrop").val());
    var clcPrice = price / count;

    $("#PerMonthInput").val(formatMoney(Math.round(clcPrice)) + " تومان");
    $("#payPerMonth").css("display", "block");
}

function CheckOfferCode() {
    debugger;
    ClacPriceByCode(0);
    $("#Register_OfferCode").css("border-color", "#ced4da");
    $("#offer-code-stat").html('');
    var code = $("#Register_OfferCode").val();
    if (code.length === 6 ) {
        Loader(1);
        $.get('/Server/GetOfferCode?code=' + code, function (res) {
            Loader(0);
            if (res.price === "-1") {
                $("#Register_OfferCode").css("border-color", "red");
                $("#offer-code-stat").html('<span class="text-danger">کد نامعتبر</span>');
            } else {
                var resPrice = parseInt(res.price);
                var codePrice = formatMoney(resPrice) + " تومان از مبلغ کلاس کم شد." ;
                $("#offer-code-stat").html('<span class="text-success">' + codePrice + '</span>');

                ClacPriceByCode(resPrice);
            }
        });
    }
}

function ClacPriceByCode(resPrice) {
    var price = $("#AppClass_Price").val();
    var priceAfterMinus = parseInt(price) - resPrice;
    $("#price-show").val(formatMoney(priceAfterMinus) + " تومان");
    if ($("#payType").val() !== "1") {
        var discounted = $("#discounted").val();
        var discountedAfterMinus = parseInt(discounted) - resPrice;
        $("#discounted-show").val(formatMoney(discountedAfterMinus) + " تومان");
    } else {
        PayPerMonth(priceAfterMinus);
    }
}

function GetTeachers(id) {
    debugger;
    Loader(1);
    $.get('/Server/GetTeachers?subjectId=' + $("#" + id).val(), function (res) {
        Loader(0);
        $("#teacherList").html(res);
    });
}