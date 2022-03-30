var
    persianNumbers = [/۰/g, /۱/g, /۲/g, /۳/g, /۴/g, /۵/g, /۶/g, /۷/g, /۸/g, /۹/g],
    arabicNumbers = [/٠/g, /١/g, /٢/g, /٣/g, /٤/g, /٥/g, /٦/g, /٧/g, /٨/g, /٩/g],
    fixNumbers = function (str) {
        if (typeof str === 'string') {
            for (var i = 0; i < 10; i++) {
                str = str.replace(persianNumbers[i], i).replace(arabicNumbers[i], i);
            }
        }
        return str;
    };

JalaliDate = {
    g_days_in_month: [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31],
    j_days_in_month: [31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29]
};

JalaliDate.jalaliToGregorian = function (j_y, j_m, j_d) {
    j_y = parseInt(j_y);
    j_m = parseInt(j_m);
    j_d = parseInt(j_d);
    var jy = j_y - 979;
    var jm = j_m - 1;
    var jd = j_d - 1;

    var j_day_no = 365 * jy + parseInt(jy / 33) * 8 + parseInt((jy % 33 + 3) / 4);
    for (var i = 0; i < jm; ++i) j_day_no += JalaliDate.j_days_in_month[i];

    j_day_no += jd;

    var g_day_no = j_day_no + 79;

    var gy = 1600 + 400 * parseInt(g_day_no / 146097); /* 146097 = 365*400 + 400/4 - 400/100 + 400/400 */
    g_day_no = g_day_no % 146097;

    var leap = true;
    if (g_day_no >= 36525) /* 36525 = 365*100 + 100/4 */ {
        g_day_no--;
        gy += 100 * parseInt(g_day_no / 36524); /* 36524 = 365*100 + 100/4 - 100/100 */
        g_day_no = g_day_no % 36524;

        if (g_day_no >= 365) g_day_no++;
        else leap = false;
    }

    gy += 4 * parseInt(g_day_no / 1461); /* 1461 = 365*4 + 4/4 */
    g_day_no %= 1461;

    if (g_day_no >= 366) {
        leap = false;

        g_day_no--;
        gy += parseInt(g_day_no / 365);
        g_day_no = g_day_no % 365;
    }

    for (var i = 0; g_day_no >= JalaliDate.g_days_in_month[i] + (i == 1 && leap); i++)
        g_day_no -= JalaliDate.g_days_in_month[i] + (i == 1 && leap);
    var gm = i + 1;
    var gd = g_day_no + 1;

    gm = gm < 10 ? "0" + gm : gm;
    gd = gd < 10 ? "0" + gd : gd;

    return [gy, gm, gd];
}


function CheckMobileFormat(that,id) {
    debugger;
    var num = that.value;
    
    if (!StringIsNullOrEmpty(num) && (num.charAt(0) !== "0" || num.charAt(1) !== "9")) {
        $("#js-error").html("فرمت شماره موبایل وارد شده اشتباه است.");
        $("#" + id).css("border-color", "red");
        $("#submit-btn").prop("disabled", true);
    } else {
        $("#js-error").html("");
        $("#" + id).css("border-color", "#ced4da");
        $("#submit-btn").prop("disabled", false);
    }
}
function CheckNationalCodeInput(that,id) {
    debugger;
    var num = that.value;
    if (!StringIsNullOrEmpty(num) && CheckNationalCode(num) === false) {
        $("#js-error").html("فرمت کدملی وارد شده اشتباه است.");
        $("#" + id).css("border-color", "red");
        $("#submit-btn").prop("disabled", true);
    } else {
        $("#js-error").html("");
        $("#" + id).css("border-color", "#ced4da");
        $("#submit-btn").prop("disabled", false);
    }
}

function CheckNationalCode(nationalCode) {
    
    if (nationalCode == null) return false;
    if (nationalCode == '') return false;
    if (nationalCode.length != 10) return false;
    if (isNaN(Number(nationalCode))) return false;

    var allDigitEqual = new Array("0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999");
    if (allDigitEqual.indexOf(nationalCode) != -1) return false;

    var chArray = nationalCode.split('');
    var num0 = Number(chArray[0].toString()) * 10;
    var num2 = Number(chArray[1].toString()) * 9;
    var num3 = Number(chArray[2].toString()) * 8;
    var num4 = Number(chArray[3].toString()) * 7;
    var num5 = Number(chArray[4].toString()) * 6;
    var num6 = Number(chArray[5].toString()) * 5;
    var num7 = Number(chArray[6].toString()) * 4;
    var num8 = Number(chArray[7].toString()) * 3;
    var num9 = Number(chArray[8].toString()) * 2;
    var a = Number(chArray[9].toString());

    var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
    var c = b % 11;

    return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
}

function formatMoney(number) {
    
    var length = number.toString().length;
    if (length > 3) {
        var strNumber = number.toString().split(".");
        var currency = "";
        var strArray = [];
        for (var i = strNumber[0].length - 1; i >= 0; i = i - 3) {
            strArray.push(strNumber[0].substring(i + 1, i - 2));
        }
        currency = strArray.reverse().join();
        if (strNumber.length > 1 && strNumber[1] !== "") {
            strNumber[1] = strNumber[1].substring(0, 2);
            currency = currency + "." + strNumber[1];
        }
        return currency;
    }
    return number;
}

$("#IconName").on('change', function () {

    if (typeof (FileReader) != "undefined") {

        var image_holder = $("#image-holder");
        image_holder.empty();
        $("#oldImg").hide();
        var reader = new FileReader();
        reader.onload = function (e) {
            $("<img />", {
                "src": e.target.result,
                "class": "img-thumbnail",
                "style": "max-width: 200px"
            }).appendTo(image_holder);

        }
        image_holder.show();
        reader.readAsDataURL($(this)[0].files[0]);
    } else {
        alert("This browser does not support FileReader.");
    }
});
$("#fileUpload").on('change', function () {

    if (typeof (FileReader) != "undefined") {

        $("#oldImg").hide();
        var image_holder = $("#image-holder");
        image_holder.empty();

        var reader = new FileReader();
        reader.onload = function (e) {
            $("<img />", {
                "src": e.target.result,
                "class": "img-thumbnail",
                "style": "max-width: 200px"
            }).appendTo(image_holder);

        }
        image_holder.show();
        reader.readAsDataURL($(this)[0].files[0]);
    } else {
        alert("This browser does not support FileReader.");
    }
});
function SetCheckBox(type) {
    
    if ($("#" + type).prop('checked') === true) {
        $("#" + type).val(true);
    }
    else {
        $("#" + type).val(false);
    }
}
$(document).ready(function () {
    
    var newversion = "Epsilon10";
    var currentversion = readCookie("VersionOfView");
    if (currentversion == null) {
        createCookie("VersionOfView", newversion);
    } else {
        if (currentversion !== newversion) {
            eraseCookie("VersionOfView");
            createCookie("VersionOfView", newversion);
            location.reload(true);
        }
    }

    ErrorHandle();
});
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    } else var expires = "";

    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(";");
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == " ") c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}

function OpenModal(url, label) {
    debugger;
    Loader(1);
    $.get(url, function (res) {
        Loader(0);
        $("#MyModal").modal('show');
        $("#ModalLabel").html(label);
        $("#MyModal-Body").html(res);
    });
}
function GetHtml(url, elem) {
    debugger;
    Loader(1);
    $.get(url, function (res) {
        Loader(0); 
        $("#" + elem).html(res);
    });
}
function Loader(show) {
    if (show === 1) {
        $('#loaderModal').modal('show');
    }
    else {
        $("#loaderModal").modal('hide');
    }
}
function GoToUrl(url) {
    
    window.location.href = url;
}
function CloseModal(url, label) {
    $("#myModal").modal('hide');
}

function ErrorMsg(title, text) {
    $.confirm({
        title: title,
        content: text,
        rtl: true,
        type: 'red',
        typeAnimated: true,
        buttons: {
            confirm: {
                text: "متوجه شدم!",
                btnClass: 'btn-red',
                action: function () {
                }
            }
        }
    });
}
function SuccessMsg(title, text) {
    $.confirm({
        title: title,
        content: text,
        rtl: true,
        type: 'green',
        typeAnimated: true,
        buttons: {
            confirm: {
                text: "متوجه شدم!",
                btnClass: 'btn-green',
                action: function () {
                }
            }
        }
    });
}

function ErrorHandle() {
    
    var msg = $("#msg").val();
    if (msg !== "" && msg !== undefined && msg !== "undefined" && msg !== null) {
        ErrorMsg("خطا!", msg);
    }
}

function StringIsNullOrEmpty(val) {
    if (val === "" || val === undefined || val === "undefined" || val === null) {
        return true;
    }
    return false;
}
function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
$("input[data-type='currency']").on({
    keyup: function () {
        formatCurrency($(this));
    },
    blur: function () {
        formatCurrency($(this), "blur");
    }
});


function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function formatCurrency(input, blur) {
    // appends $ to value, validates decimal side
    // and puts cursor back in right position.

    // get input value
    var input_val = input.val();

    // don't validate empty input
    if (input_val === "") { return; }

    // original length
    var original_len = input_val.length;

    // initial caret position 
    var caret_pos = input.prop("selectionStart");

    // check for decimal
    if (input_val.indexOf(".") >= 0) {

        // get position of first decimal
        // this prevents multiple decimals from
        // being entered
        var decimal_pos = input_val.indexOf(".");

        // split number by decimal point
        var left_side = input_val.substring(0, decimal_pos);
        var right_side = input_val.substring(decimal_pos);

        // add commas to left side of number
        left_side = formatNumber(left_side);

        // validate right side
        right_side = formatNumber(right_side);

        // On blur make sure 2 numbers after decimal
        if (blur === "blur") {
            right_side += "00";
        }

        // Limit decimal to only 2 digits
        right_side = right_side.substring(0, 2);

        // join number by .
        input_val = "" + left_side + "." + right_side;

    } else {
        // no decimal entered
        // add commas to number
        // remove all non-digits
        input_val = formatNumber(input_val);
        input_val = "" + input_val;

        // final formatting
        if (blur === "blur") {
            input_val += ".00";
        }
    }

    // send updated string to input
    input.val(input_val);

    // put caret back in the right position
    var updated_len = input_val.length;
    caret_pos = updated_len - original_len + caret_pos;
    input[0].setSelectionRange(caret_pos, caret_pos);
}
function readURL(input, id) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#' + id).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]); // convert to base64 string
    }
}

$("#imgUp").change(function () {
    readURL(this,'imgPreview');
});
$("#iconUp").change(function () {
    readURL(this,'iconPreview');
});

function DeleteFunction(url, id) {
    $.confirm({
        title: "هشدار!",
        content: "از حذف مطمئن هستید؟",
        rtl: true,
        type: 'red',
        typeAnimated: true,
        buttons: {
            confirm: {
                text: "بله",
                btnClass: 'btn-red',
                action: function () {
                    DeleteItemPost(url, id);
                }
            },
            cancel: {
                text: 'خیر',
                action: function () {
                }
            }
        }
    });
}

function DeleteItemPost(url, id) {
    debugger;
    var page = $("#PageId").val();
    if (page === undefined || page === "" || page === null) {
        page = "0";
    }
    $.post(url, { id: id, page: page }).done(function (res) {
        if (res.errorId === 0) {
            location.reload();
            return;
        } else {
            if (res.errorTitle !== "") {
                ErrorMsg('خطا', res.errorTitle);
            }
        }
    });
}

function RefreshPage() {
    window.location.reload();
}