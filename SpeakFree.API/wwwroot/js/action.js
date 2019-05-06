/**
 * Обработчик клика по ячейкам таблицы
 */
$(function () {
    $('.editable').click(function () {
        var val = $(this).html();	// получаем значение ячейки
       // $(this).addClass('editable-cell');

        if ($(this).find("input").length == 0) {
            var code = '<input type="text" id="edit" value="' + val + '" class="form-control w-100 col-12"/>'; // формируем код текстового поля            
            $(this).empty().append(code);  // удаляем содержимое ячейки, вставляем в нее сформированное поле        
            $(this).addClass('editable-cell');
            $('#edit').focus();  // устанавливаем фокус на свеженарисованное поле
            var el = document.getElementById('edit');
            el.setSelectionRange(el.value.length, el.value.length);//устанавливаем курсор в конец строки
            changeFocus();
        }
    });
});

/**
 * Отправить запрос об изменении накладной
 * @param {Object} data
 */
function changeWaybillData(data) {
    var waybillDetailListItemDto = {
        WaybillDetailId: -1,
        SortOrder: -1,
        ProductId: -1,
        ProductName: "",
        UnitName: "",
        ManufacturerCode: "",
        Declared: -1,
        Quantity: 0,
        Price: 0,
        Nds: null,
        NdsSum: null,
        TotalSum: 0,
        AcceptanceDate: null,
        AcceptorName: "",
        AcceptanceStatus: "",
        PositionNumber: 0
    };
    var isError = false;

    data.parent.each((index, value) => {
        var tabindex = value.getAttribute('tabindex');
        var boof = 0.1;
        console.log(typeof boof);
        switch (tabindex) {

        case "1":
            if ($(value).children('input').length > 0) {
                if (isNumber($(value).children('input').val())) {
                    boof = $(value).children('input').val().replace(',','.');

                    waybillDetailListItemDto.ProductId = Number(boof);
                } else {
                    resultDangerHighLiter($(value));
                    isError = true;
                    return false;
                }

            } else {
                if (isNumber(value.innerHTML)) {
                    boof = (value.innerHTML).replace(',', '.');
                    waybillDetailListItemDto.ProductId = Number(boof);
                } else {
                    resultDangerHighLiter($(value));
                    isError = true;
                    return false;
                }
            }
            break;

            case "4":
                if ($(value).children('input').length > 0) {
                    if (isNumber($(value).children('input').val())) {
                        boof = $(value).children('input').val().replace(',', '.');
                        waybillDetailListItemDto.Declared = Number(boof);
                    } else {
                        resultDangerHighLiter($(value));
                        isError = true;
                        return false;
                    }

                } else {
                    if (isNumber(value.innerHTML)) {
                        boof = (value.innerHTML).replace(',', '.');
                        waybillDetailListItemDto.Declared = Number(boof);
                    } else {
                        resultDangerHighLiter($(value));
                        isError = true;
                        return false;
                    }
                }
                break;

            case "5":
                if ($(value).children('input').length > 0) {
                    if (isNumber($(value).children('input').val())) {
                        boof = ($(value).children('input').val()).replace(',', '.');
                        waybillDetailListItemDto.Quantity = Number(boof);
                    } else {
                        resultDangerHighLiter($(value));
                        isError = true;
                        return false;
                    }
                } else {

                    if (isNumber(value.innerHTML)) {
                        boof = (value.innerHTML).replace(',', '.');
                        waybillDetailListItemDto.Quantity = Number(boof);
                    } else {
                        resultDangerHighLiter($(value));
                        isError = true;
                        return false;
                    }
                }
                break;

            case "6":

                if ($(value).children('input').length > 0) {

                    if (isNumber($(value).children('input').val())) {
                        boof = ($(value).children('input').val()).replace(',', '.');
                        waybillDetailListItemDto.Price = getCeilingByType(boof, 2);
                        $(value).children('input').val(waybillDetailListItemDto.Price);
                    } else {
                        resultDangerHighLiter($(value));
                        isError = true;
                        return false;
                    }

                } else {
                    if (isNumber(value.innerHTML)) {
                        boof = (value.innerHTML).replace(',', '.');
                        waybillDetailListItemDto.Price = getCeilingByType(boof,2);
                        value.innerHTML=(waybillDetailListItemDto.Price);
                        return false;
                    } else {
                        resultDangerHighLiter($(value));
                        isError = true;
                        return false;
                    }
                }
                break;

            default:
                break;
        }
    });

    if (!isError) {
        var position = getWaybillDetailId(data.current.parent());
        waybillDetailListItemDto.WaybillDetailId = position;
        var urlParts = window.location.href.split('/');
        var url = urlParts[0] + '//' + urlParts[2] + '/Waybills/EditDetail';
        var json = JSON.stringify(waybillDetailListItemDto);
        var token = $('input[name="__RequestVerificationToken"]').val();
        var headers = {};
        headers["__RequestVerificationToken"] = token;

        $.ajax({
            type: 'post',
            url: url,
            dataType: 'json',
            headers: headers,
            contentType: 'application/json',
            data: json
        }).done((receivedData) => {
            if (receivedData.error) {
                showError(receivedData.messages.join('<br/>'));
            //    window.location.reload();
            } else {
                changeGridData(receivedData.result, data.current);                
               // window.location.reload();
               
                setSuccessHighlight($(data.current).parent());
            }
        }).fail((error) => {
            console.error(error);
            showError('Изменения не сохранены. Подробности в консоли приложения.');
        });
    }
   
};

/**
 * Обновить данные грида
 * @param {any} parameters
 */
function updateGridData(parameters) {
    data.parent.each((index, value) => {
        var tabindex = value.getAttribute('tabindex');

        switch (tabindex) {

            case "1":
                if ($(value).children('input').length > 0) {
                    $(value).children('input').val(parameters.ProductId);
                }
                break;

            case "4":
                if ($(value).children('input').length > 0) {
                    $(value).children('input').val(parameters.Declared);
                }
                break;

            case "5":
                if ($(value).children('input').length > 0) {
                    $(value).children('input').val(parameters.Quantity);
                }
                break;

            case "6":
                if ($(value).children('input').length > 0) {
                    $(value).children('input').val(parameters.Price);
                }
                break;

            default:
                break;
        }
    });
}

/**
 * Найти id строки
 * @param {any} item
 */
function getWaybillDetailId(element) {
    var waybillDetailId = $(element).attr('data-line-id');

    return waybillDetailId;
}

/**
 * Подчеркивает зеленым цветом элемент и удаляет выделение
 * @param {any} obj
 */
function setSuccessHighlight(obj) {
    setTimeout(() => {
        $(obj).addClass('table-success');
        // $(obj).attr('class', 'table-success');
    }, 2000);

    setTimeout(() => {
        $(obj).removeClass('table-success');
    }, 5000);
}

/**
 * Подчеркивает зеленым цветом элемент и удаляет выделение
 * @param {any} obj
 */
function setErrorHighlight(obj) {
    setTimeout(() => {
        $(obj).addClass('table-danger');
    }, 2000);

    setTimeout(() => {
        $(obj).removeClass('table-danger');
    }, 10000);
}

/**
 * Обработчик нажатий на кнопки
 */
$(function () {
    $('.editable').keydown(function (event) {
        var obj = { parent: null, current: null };

        switch (event.keyCode) {
            //enter button
            case 13:
                var val = $(this).children('input').val();	// получаем то, что в поле находится
                $(this).empty().html(val); // находим ячейку, опустошаем, вставляем значение из поля
                var data = $(this).parent().children('td');
                obj.parent = $(this).parent().children('td');
                obj.current = $(this);
                changeWaybillData(obj);

                break;

            //tab button
            case 9:
                var tabindex = $(this).attr('tabindex');
                var array = $(this).parent().children('.editable');

                for (var i = 0; i < array.length; i++) {

                    if (tabindex < $(array[i]).attr('tabindex')) {
                        changeFocus();
                        $(array[i]).click();

                        break;
                    }

                    if (tabindex === $(array[i]).attr('tabindex')) {
                        changeFocus();
                        $(array[i]).click();
                    }
                };

                obj.parent = $(this).parent().children('td');
                obj.current = $(this);
                var thisObject = $(this);
                console.log(getClearData(thisObject));
                changeWaybillData(obj);

                break;

            default:
                break;
        }
    });
});

/**
 * Возвращает чистые данные из объекта
 * @param {any} obj
 */
function getClearData(obj) {
    if (obj) {
        var value = obj.children('input').val();
        return value;
    }
    return obj.value;
}

/**
 * Устанавливает обработчик на случай если фокус с элемента убран
 */
function changeFocus() {

    $('#edit').blur(function () {	// устанавливаем обработчик
        var obj = {
            parent: null,
            current: null
        };
        var val = $(this).val();	// получаем то, что в поле находится
        var parent = $(this).parent();

        $(this).parent().removeClass('editable-cell');

        if (!isNumber(val)) {
            resultDangerHighLiter($(this).parent());
        } else {
            obj.current = $(this).parent();
            obj.parent = $(this).parent().parent().children('td');
            $(this).parent().empty().html(val);  // находим ячейку, опустошаем, вставляем значение из поля
            changeWaybillData(obj);
        }

    });
}

/**
 * Проверка на то что в строке только числа
 * @param {string} val
 */
function isNumber(val) {
    var escapeChars = [];

    if (val.match(/(?!,)\D/) ) {
        escapeChars.push(1);
    }

    if (val.match(/(?!\.)\D/)) {
        escapeChars.push(1);
        
    }

    if (escapeChars.length > 1 ) {
        return false;
    }

    return true;
}

/**
 * Подсветка текста в случае ошибки ввода в числовое поле
 * @param {object} obj
 */
function resultDangerHighLiter(obj) {
    showError('В числовом поле не допускается ввод символов или букв');
    setErrorHighlight($(obj));
}

/**
 * Изменить данные формы
 * @param {Object<WaybillCountDto>} dataModel
 * @param {Object<HtmlElement>} currentElement
 */
function changeGridData(dataModel, currentElement) {

    var productNdsSum = $(currentElement).parent().children("td[tabindex='8']");
    var productTotalSum = $(currentElement).parent().children("td[tabindex='9']");
    var waybillTotalSum = $('#totalSum');
    var waybillTotalQuantity = $('#totalQuantity');

    if (dataModel) {
        $(productNdsSum).empty().html(getCeilingByType(dataModel.ndsSum,2));
        $(productTotalSum).empty().html(getCeilingByType(dataModel.totalSum,2));
        $(waybillTotalSum).empty().html(getCeilingByType(dataModel.waybillTotalSum,2));
        $(waybillTotalQuantity).empty().html(getCeilingByType(dataModel.waybillTotalQuantity,2));
    } else {
        console.log("Данные накладной не изменены !");
    }
}

/**
 * Округлить в зависимости от типа числа
 * @param {number} number входное число
 * @param {number} fixedPoints количество знаков после запятой
 */
function getCeilingByType(number, fixedPoints) {
    if (isFloat(number)) {
        return Number(number).toFixed(fixedPoints);
    } else {
        return Number(number);
    }
}

/**
 * Проверка на число с плавающей точкой
 * @param {any} n
 */
function isFloat(n) {
    return n === +n && n !== (n | 0);
}

/**
 * Проверка на целое число
 * @param {any} n
 */
function isInteger(n) {
    return n === +n && n === (n | 0);
}