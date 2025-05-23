﻿document.addEventListener("DOMContentLoaded", () => {
    const isMobile = window.innerWidth <= 768;
    if (isMobile) {
        return;
    }
    const navbar = document.querySelector(".navbar");
    window.addEventListener("scroll", () => {
        if (window.scrollY > 20) {
            navbar.classList.add("scrolled");
        } else {
            navbar.classList.remove("scrolled");
        }
    });
});