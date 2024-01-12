// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function switchVideo(key) {
    let player = document.querySelector('#youtubePlayer');
    player.src = "https://www.youtube.com/embed/" + key + "?rel=0&controls=1";

    let videoSelector = document.querySelector('#videoSelector');
    videoSelector.blur();
}
