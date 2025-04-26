import { API_URL } from './config.js';

window.updatePriceDisplay = updatePriceDisplay;

function updatePriceDisplay(value) {
    document.getElementById('priceValue').textContent = 'Максимална цена ' + value + ' лв.';
}

document.getElementById('filterForm').addEventListener('submit', function (event) {
    event.preventDefault();
    const price = document.getElementById('priceRange').value;
    const data = JSON.stringify({ price });
    const url = `${API_URL}/api/Car/FilterCarsByPrice`;

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: data
    })
        .then((res) => {
            if (res.status === 400) {
                PrintError('Невалидна цена. Моля, посочете стойност между 20 и 500 лева.');
                return null;
            } else if (res.status === 404) {
                PrintError('Няма коли в този ценови диапазон');
                return null;
            }
            return res.json();
        })
        .then((data) => {
            if (!data) return;

            const carsContainer = document.querySelector('.card-container');
            carsContainer.innerHTML = '';

            data.forEach((car) => {
                const carCard = `
                    <div class="custom-card">
                        <img src="${car.imageUrl.replace('~','')}" alt="Car Image" class="card-image">
                        <div class="card-content">
                            <h3 class="car-title">${car.brand}</h3>
                            <p class="car-subtitle">${car.model}</p>
                            <ul class="features-list">
                                ${car.features
                        .map((feature) => `<li><i class="fa fa-check"></i> ${feature.name}</li>`)
                        .join('')}
                            </ul>
                            <p class="fs-5">Цена за наемане за 1 ден</p>
                            <p class="price">${car.pricePerDay} лева</p>
                            ${car.isHired
                        ? `<a class="reserve-button" aria-disabled="true" onclick="PrintError('Колата вече е наета')">Колата вече е наета</a>`
                        : `<a class="reserve-button" href="/Car/RentACar/${car.id}">Резервирай</a>`
                    }
                        </div>
                    </div>
                `;

                carsContainer.insertAdjacentHTML('beforeend', carCard);
            });
        })
        .catch((error) => {
            console.error('Грешка:', error);
        });
});
