document.addEventListener("DOMContentLoaded", function () {
    flatpickr("#birthDate", {
        dateFormat: "d.m.Y", 
        theme: "dark", 
        altInput: true,
        altFormat: "F j, Y", 
        disableMobile: true 
    });
});