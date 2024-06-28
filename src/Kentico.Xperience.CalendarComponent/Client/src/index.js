import flatpickr from "flatpickr";
import "./styles.css";

function initializeFlatpickr(selector, options) {
    var fp = flatpickr(selector, options);
    fp.calendarContainer.onclick = function(e) {
        e.stopPropagation();
        e.preventDefault();
    };
}

window.xperience = window.xperience || {};
window.xperience.calendarComponent = {
    initializeFlatpickr: initializeFlatpickr
};