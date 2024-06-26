import flatpickr from "flatpickr";
import "./styles.css";

function initializeFlatpickr(selector, options) {
    flatpickr(selector, options);
}

window.xperience = window.xperience || {};
window.xperience.calendarComponent = {
    initializeFlatpickr: initializeFlatpickr
};