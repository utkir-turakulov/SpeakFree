'use strict';

class Tree {
    constructor(elementSelector, loadDataUrl, saveDataUrl, deleteDataUrl) {
        this.selector = elementSelector;
        this.loadUrl = loadDataUrl;
        this.saveDataUrl = saveDataUrl;
        this.deleteDataUrl = deleteDataUrl;
        this.onActivate = function (event, data) { };
        let self = this;
        $(elementSelector).fancytree({
            extensions: ['filter', 'edit', 'dnd5'],
            source: {
                url: loadDataUrl,
                cache: false
            },
            filter: {
                counter: false,
                mode: 'dimm'
            },
            dnd5: {
                autoExpandMS: 800,
                dropMarkerOffsetX: -24,
                dropMarkerInsertOffsetX: -16,
                multiSource: false,
                preventForeignNodes: true,
                preventNonNodes: true,
                preventRecursiveMoves: true,
                preventVoidMoves: true,
                scroll: true,
                dragStart: function () { return true; },
                dragEnter: function (node, data) { return ['before', 'over', 'after']; },
                dragDrop: function (node, data) {
                    if (data.otherNode) {
                        let reorder = node.parent === data.otherNode.parent && (data.hitMode == 'before' || data.hitMode == 'after');
                        data.otherNode.moveTo(node, data.hitMode);
                        if (reorder) {
                            let order = 0;
                            data.otherNode.parent.children.forEach((item) => {
                                item.data.sortPriority = order++;
                                self.saveNode(item);
                            });
                        } else {
                            if (data.hitMode == 'over') {
                                data.otherNode.data.parentId = node.key;
                            } else {
                                data.otherNode.data.parentId = node.data.parentId;
                            }
                            self.saveNode(data.otherNode);
                        }
                    }
                }
            },
            edit: {
                triggerStart: ["clickActive", "dblclick", "f2", "mac+enter", "shift+click"],
                beforeEdit: function (event, data) {
                    // Return false to prevent edit mode
                },
                edit: function (event, data) {
                    // Editor was opened (available as data.input)
                },
                beforeClose: function (event, data) {
                    // Return false to prevent cancel/save (data.input is available)
                    console.log(event.type, event, data);
                    if (data.originalEvent.type === "mousedown") {
                        // We could prevent the mouse click from generating a blur event
                        // (which would then again close the editor) and return `false` to keep
                        // the editor open:
                        //                  data.originalEvent.preventDefault();
                        //                  return false;
                        // Or go on with closing the editor, but discard any changes:
                        //                  data.save = false;
                    }
                },
                save: function (event, data) {
                    // Save data.input.val() or return false to keep editor open
                    console.log("save...", this, data);
                    data.node.title = data.input.val();
                    this.saveNode(node);
                    return true;
                },
                close: function (event, data) {
                    // Editor was removed
                    if (data.save) {
                        // Since we started an async request, mark the node as preliminary
                        $(data.node.span).addClass("pending");
                    }
                }
            },
            activate: function (event, data) {
                self.onActivate(event, data);
            }
        });
    }

    saveNode(node) {
        let data = {
            key: node.key,
            parentId: node.data.parentId,
            title: node.title,
            sortPriority: node.data.sortPriority,
            status: node.data.status,
            parameterGroupIds: node.data.parameterGroupIds
        };
        return $.post(this.saveDataUrl, data)
            .done((result) => {
                if (result.error) {
                    showError(Array.join(result.messages, '<br/>'));
                } else {
                    node.key = result.result;
                    $(node.span).removeClass("pending");
                }
            })
            .fail((error) => {
                console.error(error);
                showError("Изменения не сохранены. Подробности в консоли приложения.");
            });
    }

    addNode(title) {
        let node = $(this.selector).fancytree('getActiveNode');
        if (!node) {
            showWarning('Не выбран родительский элемент');
            return;
        }
        node.editCreateNode('child', title);
        let newNode = $(this.selector).fancytree('getActiveNode');
        newNode.data.parentId = node.key;
        newNode.data.status = 1; //draft
    }

    removeNode(message) {
        let node = $(this.selector).fancytree('getActiveNode');
        if (!node) {
            showWarning('Не выбран элемент, нечего удалять');
            return;
        }
        let self = this;
        getConfirmation(message, function () {
            let url = `${self.deleteDataUrl}/${node.key}`;
            $.post(url)
            .done((data) => {
                if (data.error) {
                    showError(Array.join(data.messages, '<br/>'));
                } else {
                    let parent = node.getParent();
                    parent.removeChild(node);
                    if (!parent.hasChildren()) {
                        parent.folder = false;
                        parent.render(true);
                    }
                    if (data.warning) {
                        showWarning(Array.jojn(data.messages, '<br/>'));
                    }
                }
            })
            .fail((error) => {
                console.error(error);
                showError('Удаление не выполнено. Подробности в консоли приложения.');
            })
        });
    }

    get currentNode() {
        return $(this.selector).fancytree('getActiveNode');
    }

    clearFilter() {
        $(this.selector).fancytree("getTree").clearFilter();
    }

    filterNodes(filterExpression) {
        $(this.selector).fancytree("getTree").filterNodes(filterExpression, { autoExpand: true, leavesOnly: false });
    }
}