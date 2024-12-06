document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.view-details-btn').forEach(button => {
        button.addEventListener('click', function () {
            const orderId = this.getAttribute('data-id');
            const orderNumber = this.getAttribute('data-order-number');

            fetch(`/Admin/Reservation/GetReservationDetails?id=${orderId}`)
                .then(response => {
                    if (!response.ok) {
                        PrintError('Възникна грешка при извличането на данни за резервацията.');
                    }
                    return response.json();
                })
                .then(data => {
                    const modalId = `reservationDetailsModal-${orderNumber}`;
                    const existingModal = document.getElementById(modalId);

                    if (existingModal) {
                        existingModal.remove();
                    }

                    const modalHtml = `
                            <div class="modal fade" id="${modalId}" tabindex="-1" aria-labelledby="${modalId}-label" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content bg-dark text-light">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="${modalId}-label">Детайли за резервация #${data.value.orderNumber}</h5>
                                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Затвори"></button>
                                        </div>
                                        <div class="modal-body fs-5">
                                            <p><strong>Номер на поръчка:</strong> ${data.value.orderNumber}</p>
                                            <p><strong>Марка на кола:</strong> ${data.value.carBrand} ${data.value.carModel}</p>
                                            <p><strong>Адрес на доставка:</strong> ${data.value.address}</p>
                                            <p><strong>Начална дата:</strong> ${data.value.startDate.toString('dd.MM.yyyy')}</p>
                                            <p><strong>Крайна дата:</strong> ${data.value.endDate}</p>
                                            <p><strong>Телефонен номер:</strong> ${data.value.phoneNumber}</p>
                                            <p><strong>Обща сума:</strong> ${data.value.totalPrice.toFixed(2)}</p>
                                            <p><strong>Детайли:</strong></p>
                                            <ul class="list-unstyled">
                                                ${data.value.insuranceBenefits.map(insuranceBenefit => `<li><i class="bi bi-check-circle text-success me-2"></i>${insuranceBenefit.name}</li>`).join('')}
                                            </ul>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Затвори</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        `;

                    document.body.insertAdjacentHTML('beforeend', modalHtml);
                    const modalElement = document.getElementById(modalId);
                    const modalInstance = new bootstrap.Modal(modalElement);
                    modalInstance.show();

                    modalElement.addEventListener('hidden.bs.modal', () => {
                        modalElement.remove();
                    });
                })
                .catch(error => {
                    PrintError('Възникна неочаквана грешка, опитай пак.');
                });
        });
    });
});