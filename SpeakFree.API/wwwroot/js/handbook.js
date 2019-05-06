'use strict';

class Handbook {
    constructor(formSelector, modalSelector, modalContentSelector, dataContentSelector, controller) {
        this.formSelector = formSelector;
        this.controller = controller;
        this.modalSelector = modalSelector;
        this.modalContentSelector = modalContentSelector;
        this.dataContentSelector = dataContentSelector;
    }

    editRecord(id) {
        let url = `${this.controller}/Get/${id}`;
        $.get(url)
            .done((data) => {
                if (data.error) {
                    showError(Array.join(data.messages, '<br/>'));
                } else {
                    $(this.modalContentSelector).html(data);
                    $(this.modalSelector).modal('show');
                }
            })
            .fail((error) => {
                console.error(error);
                showError('Ошибка обращения к серверу. Подробности в консоли приложения.');
            });
    }

    addRecord() {
        this.editRecord(0);
    }

    saveChanges() {
        let form = $(this.formSelector);
        $.ajax({
            url: `${this.controller}/Save`,
            type: 'post',
            data: form.serialize()
        })
            .done((data) => {
                if (data.error) {
                    showError(Array.join(data.messages, '<br/>'));
                } else {
                    $(this.dataContentSelector).html(data);
                    $(this.modalSelector).modal('hide');
                }
            })
            .fail((error) => {
                console.error(error);
                showError('Ошибка при сохранении элемента справочника. Подробности в консоли приложения.');
            });
    }

    deleteRecord(id) {
        let self = this;
        getConfirmation("Вы действительно хотите удалить запись?", function () {
            let url = `${self.controller}/Delete/${id}`;
            $.post(url)
                .done((data) => {
                    if (data.error) {
                        showError(Array.join(data.messages, '<br/>'));
                    } else {
                        $(self.dataContentSelector).html(data);
                    }
                })
                .fail((error) => {
                    console.error(error);
                    showError('Ошибка при удалении элемента справочника. Подробности в консоли приложения.');
                });
        });
    }
}