document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.save-role-btn').forEach(button => {
        button.addEventListener('click', function () {
            const userId = this.getAttribute('data-userid');
            const modalId = this.getAttribute('data-modalid');
            const username = this.getAttribute('data-username');
            const roleSelect = document.getElementById(`role-${userId}`);
            const selectedRole = roleSelect ? roleSelect.value : null;
            const option = document.getElementById(`option-${userId}`);

            if (!selectedRole) {
                alert('Моля, изберете роля.');
                return;
            }

            console.log(`Задаване на роля '${selectedRole}' за потребител с ID '${userId}'`);
            const data = {
                RoleName: selectedRole,
                UserId: userId
            };

            fetch('/Admin/User/SetUserRole',
                {
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                }).then(async res => {
                    if (res.ok) {
                        const rolesList = document.getElementById(`roles-list${userId}`);
                        const newRoleItem = document.createElement('li');
                        newRoleItem.className = 'd-flex align-items-center mb-2';
                        newRoleItem.innerHTML = `
                        <span>${selectedRole}</span>
                        <button type="button" class="btn btn-sm btn-danger ms-2 remove-role-btn" data-userid="${userId}" data-role="${selectedRole}" data-username="${username}">
                        <i class="bi bi-x-circle"></i>
                        </button>
                       `;
                        rolesList.appendChild(newRoleItem);

                        const assignRoleModal = bootstrap.Modal.getInstance(document.getElementById(modalId));
                        if (assignRoleModal) {
                            assignRoleModal.hide();
                        }

                        const backdrop = document.querySelector('.modal-backdrop');
                        if (backdrop) {
                            backdrop.remove();
                        }

                        addRemoveRoleEvent(newRoleItem.querySelector('.remove-role-btn'));
                        option.remove();
                        PrintAlert(`Ролята ${selectedRole} e успешно добавена.`);
                    } else {
                        const data = await res.json();
                        if (data.status) {
                            PrintError(data.status);
                        }
                        const assignRoleModal = bootstrap.Modal.getInstance(document.getElementById(modalId));
                        if (assignRoleModal) {
                            assignRoleModal.hide();
                        }

                        const backdrop = document.querySelector('.modal-backdrop');
                        if (backdrop) {
                            backdrop.remove();
                        }
                    }
                });


        });
    });

    function addRemoveRoleEvent(button) {
        button.addEventListener('click', function () {
            const userId = this.getAttribute('data-userid');
            const role = this.getAttribute('data-role');
            const username = this.getAttribute('data-username');
            const roleSelector = document.getElementById(`role-${userId}`);
            const deleteData = {
                RoleName: role,
                UserId: userId
            };
            Swal.fire({
                title: `Премахване на роля '${role}' за потребител ${username}'`,
                icon: 'warning',
                background: '#343a40',
                color: '#f8f9fa',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Да, премахни ролята',
                cancelButtonText: 'Отказ'
            }).then((result) => {
                if (result.isConfirmed) {
                    fetch('/Admin/User/RemoveUserRole',
                        {
                            method: "POST",
                            headers: {
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify(deleteData)
                        })
                        .then(async res => {
                            if (res.ok) {
                                this.closest('li').remove();
                                const optionElement = document.createElement('option');
                                optionElement.innerHTML = `<option id="option-${userId}" value="${role}">${role}</option>`;
                                roleSelector.append(optionElement);
                                PrintAlert(`Ролята ${role} e успешно премахната.`);
                            } else {
                                const data = await res.json();
                                if (data.status) {
                                    PrintError(data.status);
                                }
                            }
                        });
                }
            });
        });
    }

    document.querySelectorAll('.remove-role-btn').forEach(addRemoveRoleEvent);
    document.querySelectorAll('.delete-user-btn').forEach(button => {
        button.addEventListener('click', function () {
            const userId = this.getAttribute('data-userid');
            const username = this.getAttribute('data-userUsername');
            const deletionData = {
                Id: userId
            };

            Swal.fire({
                title: `Сигурен ли си, че искаш да изтриеш акаунт с потребителско име '${username}'?`,
                icon: 'warning',
                background: '#343a40',
                color: '#f8f9fa',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Да, продължи',
                cancelButtonText: 'Отказ'
            }).then((result) => {
                if (result.isConfirmed) {
                    fetch('/Admin/User/RemoveUser', {
                        method: "POST",
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(deletionData)
                    })
                        .then(async res => {
                            if (res.ok) {
                                this.closest('tr').remove();
                                PrintAlert('Акаунтът беше успешно изтрит.');
                            } else {
                                const data = await res.json();
                                if (data.status) {
                                    PrintError(data.status);
                                }
                            }
                        })
                        .catch(error => {
                            console.error('Грешка при заявката:', error);
                        });
                }
            });
        });
    });

});