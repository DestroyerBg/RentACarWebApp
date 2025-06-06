﻿import { API_URL } from './config.js';
const locationButton = document.getElementById('useLocationButton');

locationButton.addEventListener('click', () => GetLocation());

function GetLocation() {
    if (!navigator.geolocation) {
        alert('Вашият браузър не поддържа Geolocation');
        return;
    }

    navigator.geolocation.getCurrentPosition(
        async (position) => {
            const latitude = position.coords.latitude;
            const longitude = position.coords.longitude;


            const url = `${API_URL}/api/geolocation/reverse-geocode?latitude=${latitude}&longitude=${longitude}`;

            let data;
            fetch(url)
                .then((response) => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }
                    return response.json(); 
                })
                .then((data) => {
                    const formattedAddress = data.display_name || "Адресът не може да бъде намерен.";
                    const addressInput = document.getElementById('addressInput');
                    if (addressInput) {
                        addressInput.value = formattedAddress;
                    } else {
                        console.error("Елементът 'addressInput' не е намерен в DOM.");
                    }
                })
                .catch((error) => {
                    console.error("Грешка при намиране на адреса:", error);
                });
        });
}