import flatpickr from "flatpickr";
import "./styles.css";

function initializeFlatpickr(selector, options) {
    var fp = flatpickr(selector, options);
    fp.calendarContainer.onclick = function(e) {
        e.stopPropagation();
        e.preventDefault();
    };
}

function areSameDay(d1, d2) {
    return d1.getFullYear() === d2.getFullYear() &&
        d1.getMonth() === d2.getMonth() &&
        d1.getDate() === d2.getDate();
}

async function fetchExcludedDateTimeData(dataProviderName) {
    try {
        let response = await fetch(`/kentico.calendarComponent/GetExcludedDateTimeData?dataProviderName=${encodeURIComponent(dataProviderName)}`);
        if (response.ok) {
            let data = await response.json();
            return data;
        } else {
            throw new Error('Failed to fetch data');
        }
    } catch (error) {
        console.error('Error fetching data:', error);
        return {
            excludedDates: [],
            excludedTimeFrames: []
        };
    }
}

function isTimeInRange(selectedDateTime, excludedFrame, minuteIncrement) {
    const time = selectedDateTime.getHours() * 60 + selectedDateTime.getMinutes();

    const startMinutes = excludedFrame.getHours() * 60 + excludedFrame.getMinutes();
    const endMinutes = startMinutes + minuteIncrement;

    return time >= startMinutes && time < endMinutes;
}

function isExcludedTime(date, excludedTimeFrames, minuteIncrement) {
    for (const frame of excludedTimeFrames) {

        if (areSameDay(date, frame)) {
            if (isTimeInRange(date, frame, minuteIncrement)) {
                return true;
            }
        }
    }
    return false;
}

function handleDateTimeChange(selectedDates, dateStr, instance, excludedTimeFrames, lastSelected) {
    const selectedDate = selectedDates[0];
    const last = new Date(lastSelected);
    const adjustedDate = new Date(selectedDate);

    if (selectedDate && isExcludedTime(selectedDate, excludedTimeFrames, instance.config.minuteIncrement)) {
        if (adjustedDate < last) {
            adjustedDate.setMinutes(adjustedDate.getMinutes() - instance.config.minuteIncrement);
        }
        else {
            adjustedDate.setMinutes(adjustedDate.getMinutes() + instance.config.minuteIncrement);
        }

        instance.setDate(adjustedDate, true);

        return adjustedDate;
    }

    return adjustedDate;
}

async function initializeFlatpickrSingleInput(configuration) {
    let excludedDateTimeData = await fetchExcludedDateTimeData(configuration.provider);

    const flatpickrOptions = configuration.flatpickrOptions;
    const time = configuration.selectedDate;
    const dateOnly = configuration.dateOnly;
    const timeZoneInput = document.getElementById(configuration.timeZoneInputId);

    flatpickrOptions.disable = excludedDateTimeData.excludedDates.map((x) => new Date(x));
    flatpickrOptions.minTime = excludedDateTimeData.minTime;
    flatpickrOptions.maxTime = excludedDateTimeData.maxTime;
    flatpickrOptions.minDate = excludedDateTimeData.minDate;
    flatpickrOptions.maxDate = excludedDateTimeData.maxDate;

    if (!dateOnly) {
        var serverDate = new Date(time);

        if (configuration.displayDateTimeInClientTimeZone) {
            var serverOffset = serverDate.getTimezoneOffset() * 60000;
            var clientOffset = (new Date().getTimezoneOffset()) * 60000;

            var offsetDifference = serverOffset - clientOffset;
            var displayDate = new Date(serverDate.getTime() + offsetDifference);
            flatpickrOptions.defaultDate = [displayDate];

            timeZoneInput.value = new Date().getTimezoneOffset();
        } else {
            flatpickrOptions.defaultDate = [serverDate];
            timeZoneInput.value = serverDate.getTimezoneOffset();
        }
    } else {
        timeZoneInput.value = new Date(time).getTimezoneOffset();
    }

    let disabledDateTimes = excludedDateTimeData.excludedTimeFrames.map((x) => new Date(x));
    let onChange = [];
    let lastSelected = new Date(flatpickrOptions.defaultDate[0]);

    onChange.push(function (selectedDates, dateStr, instance) {
        lastSelected = handleDateTimeChange(selectedDates, dateStr, instance, disabledDateTimes, lastSelected);
    });

    flatpickrOptions.onChange = onChange;

    initializeFlatpickr("#" + configuration.calendarId, flatpickrOptions);
}

async function initializeFlatpickrMultiInput(configuration) {
    let excludedDateTimeData = await fetchExcludedDateTimeData(configuration.provider);

    let flatpickrOptions = configuration.flatpickrOptions;
    let excludedDates = excludedDateTimeData.excludedDates.map((x) => new Date(x));

    let minDate = new Date(excludedDateTimeData.minDate);
    let maxDate = new Date(excludedDateTimeData.maxDate);

    flatpickrOptions.disable = [
        function (date) {
            return excludedDates.some((x) => areSameDay(x, date)) || date < minDate || date > maxDate;
        }
    ];

    initializeFlatpickr("#" + configuration.calendarId, flatpickrOptions);
}

window.xperience = window.xperience || {};
window.xperience.calendarComponent = {
    initializeFlatpickrMultiInput: initializeFlatpickrMultiInput,
    initializeFlatpickrSingleInput: initializeFlatpickrSingleInput
};