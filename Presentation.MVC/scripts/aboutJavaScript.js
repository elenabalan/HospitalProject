if (!jQuery) { throw new Error("Bootstrap requires jQuery") }

$(document).ready(function () {
 
});



debugger;
function BorderImg(doc) {
    debugger;
    $("#"+doc).css("border", "1px solid green");
}

function NoBorderImg(doc) {
    $("#"+doc).css("border", "none");
}

function ChangeText(docId) {
    debugger;

    $(".fotoDoctorTextHidden").hide();
    $(docId).show();
  
}



//function BorderImg(doc) {
//    document.getElementById(doc).style.border = "1px solid green";
//}

//function NoBorderImg(doc) {
//    document.getElementById(doc).style.border = "none";
//}

//function ChangeText(docId) {
//    var toHide = document.getElementsByClassName('show');

//    var toShow = document.getElementById(docId);

//    if (toHide.length != 0)
//        document.getElementsByClassName('show')[0].className = 'fotoDoctorTextHidden';
//    if (toShow != null)
//        toShow.className = "show";
//}