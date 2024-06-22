// JavaScript to show the loader when content is loading
function showLoader() {
    $("#overlay").fadeIn(300);
}

// JavaScript to hide the loader when content is loaded
function hideLoader() {
    setTimeout(function () {
        $("#overlay").fadeOut(300);
    }, 500);
}

