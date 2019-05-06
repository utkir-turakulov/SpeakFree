// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function setSelectList(list, select) {
    select.selectedIndex = -1;
    select.options.length = 0;
    for (index in list) {
        select.options[select.options.length] = new Option(list[index].text, list[index].value);
    }
}

function setEditableSelectList(list, select) {
    select.editableSelect('clear');
    for (index in list) {
        select.editableSelect('add', list[index].text, index, null, `value="${list[index].value}"`);
    }
}

function showNotification(title, message, type, css) {
    let note = new PNotify({
        title: title,
        text: message,
        type: type,
        addclass: css + ' alert alert-styled-left alert-arrow-left',
        animate_speed: 'fast',
        buttons: {
            closer_hover: false,
            sticker_hover: false
        }
    });
}

function getConfirmation(message, action) {
    var notice = new PNotify({
        title: 'Подтверждение',
        text: message,
        hide: false,
        type: 'warning',
        addclass: 'bg-warning alert alert-styled-left alert-arrow-left',
        animate_speed: 'fast',
        confirm: {
            confirm: true,
            buttons: [
                {
                    text: 'Да',
                    addClass: 'btn btn-sm btn-primary'
                },
                {
                    text: 'Нет',
                    addClass: 'btn btn-sm btn-link'
                }
            ]
        },
        buttons: {
            closer: false,
            sticker: false
        }
    });
    notice.get().on('pnotify.confirm', function () {
        action();
    });
    notice.get().on('pnotify.cancel', function (e, notice) {
        notice.cancelRemove().update({
            title: 'Информация',
            text: 'Действие отменено',
            type: 'info',
            addclass: 'bg-info alert alert-styled-left alert-arrow-left',
            animate_speed: 'fast',
            hide: true,
            delay: 2000,
            confirm: {
                confirm: false
            },
            buttons: {
                closer: true,
                sticker: true
            }
        });
    });
}

function showSuccess(message) {
    showNotification('Подтверждение', message, 'success', 'bg-success');
}

function showInfo(message) {
    showNotification('Информация', message, 'info', 'bg-info');
}

function showWarning(message) {
    showNotification('Предупреждение', message, 'warning', 'bg-warning');
}

function showError(message) {
    showNotification('Ошибка!', message, 'error', 'bg-danger');
}

var _alert;
function consume_alert() {
    if (_alert) return;
    _alert = window.alert;
    window.alert = function (message) {
        showNotification('Сообщение', message, '', 'bg-primary');
    };
}

var currentValue = null;
function saveCurrentValue(element) {
    currentValue = $(element).val();
}

function blockElement(element) {
    $(element).block({
        message: '<i class="icon-spinner2 spinner"></i>',
        overlayCSS: {
            backgroundColor: '#fff',
            opacity: 0.8,
            cursor: 'wait',
            'box-shadow': '0 0 0 1px #ddd'
        },
        css: {
            border: 0,
            padding: 0,
            backgroundColor: 'none'
        }
    });
}

$(document).ready(function () {
    consume_alert();
});
