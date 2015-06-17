/*//#region description
                version : 2.1
                use: 
                    1. instance new ImageCropTool, example: var z = new ImageCropTool;
                    2. setup options, example: var myOptions = { 
                            container:"#myContainer", // id of node element where to hookup ImageCropTool control
                            btnOpen: {text:"MyName", attr:{"class":"colorIt"}} // display text of button and additional attributes
                            };
                    3. start initialization with options, example: z.init(myOptions);
                Log: 
                    v1.11: variable scope error solved, all variables convertet to object properties
                    v1.12: added functionality get result when selection is made (without buttons preview and save),
                            watch function for automatic onchange fire event
                    v2.0:
                    v2.1: resolved wrong calculation of element offset
                    selection handels:
                    0  1  2
                    3  4  5
                    6  7  8
//#endregion
*/

/*USAGE
            // avatar 360X270
            avatarTool = new ImageCropTool("#avatarCropTool");

            avatarTool['options.container.toolbox.attr'] = { "class": "btn-group"};
            avatarTool.options.container.toolbox.buttons.open.text = "Izaberite sliku";
            avatarTool.options.container.toolbox.buttons.open.attr = { "class": "btn btn-primary" };

            avatarTool.options.control.toolbox.buttons.close.text = "Zatvori";
            avatarTool.options.control.toolbox.buttons.save.text = "Spremi odabir"
            avatarTool.options.control.toolbox.buttons.save.callback = function () {
                var b64 = this.getImageB64();

                this.removeSelf();
                
                model.addAvatar(b64);
            };
            avatarTool.options.control.toolbox.buttons.preview.show = false;
            avatarTool.options.control.result.size = {"width":"360","height":"270"};

            avatarTool.init();

            picTool = new ImageCropTool("#pictureCropTool");
            picTool.options.container.attr = { "class": "span1" };
            picTool.options.container.toolbox.buttons.open.attr = { "class": "btn btn-info", "data-follower": "follow" };
            picTool.options.container.toolbox.buttons.open.html = '<i class="icon-plus icon-white"></i>';
            picTool.options.control.attr = { "class": "offset1 thumbnail" };
            picTool.options.control.toolbox.buttons.preview.text = "Pregled";
            picTool.options.control.toolbox.buttons.save.text = "Prikaži u galeriji"
            picTool.options.control.toolbox.buttons.close.text = "Zatvori";
            picTool.options.control.toolbox.buttons.save.callback = function () {
                var b64 = this.getImageB64(),
                    desc = this.getDescription(),
                    date = this.getDate();

                model.addPicture(b64, desc, date);
            };
            picTool.options.control.title.text = "Slika #";
            picTool.options.control.title.show = true;
            picTool.options.control.title.attr = { "class": "label pull-right" };
            picTool.options.control.scene.input.show = true;
            picTool.options.control.scene.input.attr = { "class": "row-fluid" };
            picTool.options.control.scene.input.controls.datepicker.attr.extendObject({ "class": "span3" });
            picTool.options.control.scene.input.controls.description.attr.extendObject({ "class": "input-block-level span9", "placeholder": "Unesite opis..." });
            picTool.options.control.result.size = { "width": "800", "height": "600" };

            picTool.init();
        });
*/

Object.defineProperty(Object.prototype, "extendObject", {
    value: function (options) {
        for (var i in options) {
            if (typeof options[i] == "object") {
                if (!this.hasOwnProperty(i)) {
                    this[i] = {};
                }
                this.extendObject(options[i]);
            } else {
                this[i] = options[i];
            }
        }
        return this;
    },
    writable: false,
    enumerable: false
});
Object.defineProperty(Object.prototype, "getTotalOffsetLeft", {
    value: function (element) {
        var a = element,
            b = 0;

        while (a) {
            b += a.offsetLeft;
            a = a.offsetParent;
        }

        return b;
    },
    writable: false,
    enumerable: false
});
Object.defineProperty(Object.prototype, "getTotalOffsetTop", {
    value: function (element) {
        var a = element,
            c = 0;

        while (a) {
            c += a.offsetTop;
            a = a.offsetParent;
        }

        return c;
    },
    writable: false,
    enumerable: false
});

(function () {
    var OPTIONS = function () {
        this.container = {
            type: "div",
            attr: {},
            title: { type: "span", text: "Image Crop Tool", attr: {}, show: false },
            //controls: { type: "div", text: "", attr: {} },
            toolbox: {
                type: "div",
                attr: {},
                show: true,
                buttons: {
                    open: {
                        text: "Select pictures",
                        html: "",
                        type: "button",
                        show: true,
                        attr: {
                            "type": "button"
                        },
                        callback: null
                    },
                    save: {
                        text: "Save pictures",
                        type: "button",
                        show: true,
                        attr: {},
                        callback: null
                    },
                    _choose: {
                        type: "input",
                        selectMultipleFiles: false,
                        attr: {
                            "type": "file",
                            "accept": "image/*",
                            "style": "display:none !important;"
                        },
                        externalElementRef: null
                    }
                }
            }
        };
        this.control = {
            type: "div", attr: {}, title: { type: "h5", text: "Scene #", attr: {}, show: false },

            toolbox: {
                type: "div", show: true, attr: { "class": "btn-group" },

                buttons: {
                    open: {
                        text: "Select picture",
                        type: "button",
                        show: true,
                        attr: { "class": "btn" },
                        callback: null
                    },
                    preview: {
                        text: "Preview selection",
                        type: "button",
                        show: true,
                        attr: { "class": "btn" },
                        callback: function () { this.addScenePreview(); }
                    },
                    save: {
                        text: "Save picture",
                        type: "button",
                        show: true,
                        attr: { "class": "btn" },
                        callback: null
                    },
                    rotate: {
                        text: "Rotate right",
                        type: "button",
                        show: false,
                        attr: {},
                        callback: null
                    },
                    close: {
                        text: "Close",
                        type: "button",
                        show: true,
                        attr: { "class": "btn" },
                        callback: function () { this.removeSelf(); }
                    },
                    _choose: {
                        type: "input",
                        attr: {
                            "type": "file",
                            "accept": "image/*",
                            "style": "display:none !important;"
                        }
                    }
                }
            },
            scene: {
                type: "div", attr: { "class": "row" },

                input: {
                    type: "div",
                    show: true,
                    attr: {},

                    controls: {
                        datepicker: {
                            type: "input",
                            show: false,
                            attr: {
                                "type": "date"
                            }
                        },
                        title: {
                            type: "input",
                            show: true,
                            attr: {
                            }
                        },
                        description: {
                            type: "textarea",
                            show: true,
                            attr: {
                                "rows": "1",
                                "placeholder": "Add some description..."
                            }
                        }
                    }
                },
                edit: {
                    type: "div",
                    attr: { "class": "col-md-6" },

                    controls: {
                        edit: {
                            show: true,
                            attr: { width: 400, height: 400 },
                            selection: {
                                aspectRatio: {
                                    lockRatio: true
                                },
                                selectors: {
                                    size: 10,
                                    fill_color: "rgba(255,255,255,0.5)",
                                    mouseover: ['nw-resize', 'n-resize', 'ne-resize', 'w-resize', 'move', 'e-resize', 'sw-resize', 's-resize', 'se-resize'],
                                    active_zone: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                                }
                            }
                        }
                    }
                },
                preview: {
                    type: "div",
                    show: true,
                    attr: { "class": "col-md-6" },

                    controls: {
                        preview: {
                            attr: {
                                //style: "width: 400px; height: 400px;"
                            }
                        }
                    }
                }
            },
            result: {
                size: {
                    width: 300,
                    height: 200
                },
                type: "image/jpeg",
                quality: 0.8
            }
        };
    };

    // base of all controls
    var asControl = (function () {
        var createel = function () {
            this.element = document.createElement(this.options.type);
            if (this.options.attr && Object.getOwnPropertyNames(this.options.attr).length > 0) {
                for (var key in this.options.attr) {
                    this.element.setAttribute(key, this.options.attr[key]);
                }
            }
            if (this.options.text) { this.element.innerText = this.options.text; }
            if (this.options.html) { this.element.innerHTML = this.options.html; }
            return this;
        },
        removeel = function () {
            if (this.element && this.element.parentNode) {
                this.element.parentNode.removeChild(this.element);
            }
            return this;
        },
        log = function (/*optional*/msg) {
            console.log(Date.now() + " | " + this.name + " = " + msg + JSON.stringify(this, this.logProps));
            return this;
        },
        appendch = function (elm) {
            //if (elm instanceof _Control) {
            this.element.appendChild(elm.element);
            //} else {
            //    this.element.appendChild(elm);
            //}

            return this;
        },
        prependch = function (elm) {
            // TODO: cant prepend on startup because it not exisist
            this.element.insertBefore(elm.element, this.element);
            return this;
        },
        addeventlis = function (event, callback, /*optional*/bind) {
            if (bind) {
                this.element.addEventListener(event, callback.bind(bind), false);
            } else {
                this.element.addEventListener(event, callback, false);//.bind(this)
            }
            return this;
        },
        attachElement = function (element) {
            this.element = element;

            return this;
        }
        ;

        return function () {
            this.createSelf = createel;
            this.removeSelf = removeel;
            this.log = log;
            this.appendChild = appendch;
            this.prependChild = prependch;
            this.addEventListener = addeventlis;
            this.attachElement = attachElement;
            return this;
        };
    })();
    var _Control = function (options, name, logProps) {
        this.name = name || "noname";
        this.element = null;
        this.options = options || {};
        this.logProps = logProps || ["width", "height"];
    };
    asControl.call(_Control.prototype);

    var asContainer = (function () {
        var
        addTitle = function (/*optional*/id) {
            if (this.options.title && this.options.title.show) {
                var t = document.createElement(this.options.title.type),
                    ops = this.options.title;

                if (ops.attr && Object.getOwnPropertyNames(ops.attr).length > 0) {
                    for (var key in ops.attr) {
                        t.setAttribute(key, ops.attr[key]);
                    }
                }
                if (ops.text) {
                    t.innerText = (/\#/.test(ops.text)) ? ops.text.replace(/\#/, id) : ops.text;
                }

                this.element.appendChild(t);
            }

            return this;
        },
        init = function () {
            this.createSelf();
            return this;
        };

        return function () {
            this.addTitle = addTitle;
            this.init = init;
            return this;
        };
    })();
    var _Container = function (options, name, logProps) {
        _Control.call(this, options, name, logProps);
    };
    asControl.call(_Container.prototype);
    asContainer.call(_Container.prototype);

    var asToolbox = (function () {
        var
        addTbox = function (bind) {
            if (!this.options.show) {
                return this;
            }

            var cbo = this.options,
                bto = this.options.buttons,
                me = (bind) ? bind : this,
                btnChooseFile;

            if (bto && bto._choose && this.onOpen) {
                btnChooseFile = bto._choose.externalElementRef ?
                     new _Control(bto._choose, "ChooseFile").attachElement(bto._choose.externalElementRef)
                     : new _Control(bto._choose, "ChooseFile").createSelf();
                if (bto._choose.selectMultipleFiles) { btnChooseFile.element.setAttribute("multiple", "multiple"); }
                btnChooseFile.addEventListener('change', this.onOpen, me);
                if (bto._choose.externalElementRef == null) this.appendChild(btnChooseFile);
            }

            if (bto && bto.open && bto.open.show && this.onOpen) {
                var btnOpen = new _Control(bto.open, "Open").createSelf();
                btnOpen.addEventListener('click', function () {
                    btnChooseFile.element.click();
                });
                this.appendChild(btnOpen);
            }

            if (bto && bto.preview && bto.preview.show && this.onPreview) {
                var btnPreview = new _Control(bto.preview, "Preview").createSelf();
                btnPreview.addEventListener('click', this.onPreview, me);
                this.appendChild(btnPreview);
            }

            if (bto && bto.save && bto.save.show && this.onSave) {
                var btnSave = new _Control(bto.save, "Save").createSelf();
                //TODO: implement save event
                btnSave.addEventListener('click', this.onSave, me);
                this.appendChild(btnSave);
            }

            if (bto && bto.rotate && bto.rotate.show && this.onRotate) {
                var btnRotate = new _Control(bto.rotate, "Rotate").createSelf();
                //TODO: implement rotate event
                btnRotate.addEventListener('click', this.onRotate, me);
                this.appendChild(btnRotate);
            }

            if (bto && bto.close && bto.close.show && this.onClose) {
                var btn = new _Control(bto.close, "Preview").createSelf();
                btn.addEventListener('click', this.onClose, me);
                this.appendChild(btn);
            }

            return this;
        },
        init = function () {
            this.createSelf();
            return this;
        };

        return function () {
            this.init = init;
            this.addToolbox = addTbox;
            return this;
        };
    })();
    var _Toolbox = function (options, name, onOpen, onSave, onPreview, onRotate, onClose, logProps) {
        _Control.call(this, options, name, logProps);
        this.onOpen = onOpen;
        this.onSave = onSave;
        this.onPreview = onPreview;
        this.onRotate = onRotate;
        this.onClose = onClose;
    };
    asControl.call(_Toolbox.prototype);
    asToolbox.call(_Toolbox.prototype);

    var asImage = (function () {
        var procBlob = function (blob) {
            var me = this;
            this.element.onload = function () {
                if (me.onload) {
                    me.onload(this);
                }
                me.log("Loaded image ");
            };

            this.element.src = blob;
            return this;
        },
        base64 = function (base) {
            this.element.src = base;
            return this;
        },
        init = function () {
            this.createSelf();
            return this;
        };

        return function () {
            this.init = init;
            this.processBlobUrl = procBlob;
            this.fromBase64 = base64;
            return this;
        };
    })();
    var _Image = function (options, name, logProps, onload) {
        _Control.call(this, options, name, logProps);
        this.options.type = "img";
        this.onload = onload;
        this.logProps = ["element", "width", "height"];
    };
    asControl.call(_Image.prototype);
    asImage.call(_Image.prototype);

    var asCanvas = (function () {
        var
        draw = function () {
            var canvas = this.element;

            this.ctx.clearRect(0, 0, canvas.width, canvas.height);
            this.ctx.drawImage(this.Image.element, 0, 0, canvas.width, canvas.height);

            return this;
        },
        image = function (image) {
            this.Image = image;
            return this;
        },
        remesure = function () {
            var ow = this.options.attr.width,
                oh = this.options.attr.height,
                iw = this.Image.element.width,
                ih = this.Image.element.height,
                scale = this.scale;

            if (iw > ow) {
                scale = ow / iw;
            } else if (ih > oh) {
                scale = oh / ih;
            }

            this.element.width = this.width = iw * scale;
            this.element.height = this.height = ih * scale;
            this.scale = scale;

            return this;
        },
        rotate = function () {
            var data = this.ctx.getImageData(0, 0, this.width, this.height),
                nw = this.height,
                nh = this.width;

            this.element.width = nw;
            this.element.height = nh;
            this.width = nw;
            this.height = nh;

            this.ctx.save();
            // translate context to center of canvas
            this.ctx.translate(nw, 0);

            // rotate 90 degrees clockwise
            this.ctx.rotate(90 * Math.PI / 180);
            this.ctx.putImageData(data, 0, 0);
            //draw image
            this.ctx.restore();
        },
        writeText = function (message, x, y) {
            this.ctx.font = '11pt Calibri';
            this.ctx.fillStyle = 'white';
            this.ctx.fillText(message, x, y);
            return this;
        },
        init = function () {
            this.createSelf();
            this.ctx = this.element.getContext('2d');
            return this;
        };

        return function () {
            this.init = init;
            this.image = image;
            this.draw = draw;
            this.rotate = rotate;
            this.reMesure = remesure;
            this.writeText = writeText;
            return this;
        };
    })();
    var _Canvas = function (options, name, logProps) {
        _Control.call(this, options, name, logProps);
        this.options.type = "canvas";
        this.Image = null;
        this.ctx = null;
        this.width = 0;
        this.height = 0;
        this.scale = 1;
    };
    asControl.call(_Canvas.prototype);
    asCanvas.call(_Canvas.prototype);

    var asSelection = (function () {
        var calcSel = function (newcords) {
            var cord = this.cordinates,
                ratio = this.options.selection.aspectRatio,
                rezsize = this.result_size;

            newcords.x = (newcords.x || newcords.x === 0) ? newcords.x : cord.x;
            newcords.y = (newcords.y || newcords.y === 0) ? newcords.y : cord.y;
            newcords.w = (newcords.w) ? newcords.w : cord.w;
            newcords.h = (newcords.h) ? newcords.h : cord.h;

            if (newcords.x >= 0 && newcords.w > 0 && newcords.x + newcords.w <= this.width && newcords.y >= 0 &&
                newcords.h > 0 && newcords.y + newcords.h <= this.height) {
                cord.x = Math.round(newcords.x);
                cord.y = Math.round(newcords.y);
                cord.w = Math.round(newcords.w);
                cord.h = Math.round(newcords.h);
            }
            return this;
        },
        calcSelHnd = function () {
            var cord = this.cordinates,
                ses = this.handels,
                hsz = this.options.selection.selectors.size;
            //selection.selectors.handels calculator
            //0  1  2
            //3  4  5
            //6  7  8
            ses[0].x = cord.x;
            ses[3].x = cord.x;
            ses[6].x = cord.x;

            ses[1].x = cord.x + cord.w / 2 - hsz / 2;
            ses[4].x = cord.x + cord.w / 2 - hsz / 2;
            ses[7].x = cord.x + cord.w / 2 - hsz / 2;

            ses[2].x = cord.x + cord.w - hsz;
            ses[5].x = cord.x + cord.w - hsz;
            ses[8].x = cord.x + cord.w - hsz;

            ses[0].y = cord.y;
            ses[1].y = cord.y;
            ses[2].y = cord.y;

            ses[3].y = cord.y + cord.h / 2 - hsz / 2;
            ses[4].y = cord.y + cord.h / 2 - hsz / 2;
            ses[5].y = cord.y + cord.h / 2 - hsz / 2;

            ses[6].y = cord.y + cord.h - hsz;
            ses[7].y = cord.y + cord.h - hsz;
            ses[8].y = cord.y + cord.h - hsz;

            return this;
        },
        selDraw = function () {
            var ctx = this.ctx,
                cord = this.cordinates,
                opsSel = this.options.selection.selectors;

            // makes image little darker
            ctx.fillStyle = 'rgba(0, 0, 0, 0.5)';
            ctx.fillRect(0, 0, this.width, this.height);

            ctx.strokeStyle = '#fff';
            if (ctx.setLineDash) {
                ctx.setLineDash([10, 5]);
            }
            ctx.lineWidth = 2;
            ctx.strokeRect(cord.x, cord.y, cord.w, cord.h);
            // draw part of original image
            ctx.drawImage(
                this.image,
                cord.x / this.scale,
                cord.y / this.scale,
                cord.w / this.scale,
                cord.h / this.scale,
                cord.x, cord.y, cord.w, cord.h);

            // draw resize handels
            ctx.fillStyle = opsSel.fill_color;
            for (var i = 0; i < opsSel.active_zone.length; i++) {
                var j = opsSel.active_zone[i];
                ctx.fillRect(this.handels[j].x, this.handels[j].y, opsSel.size, opsSel.size);
            }
            return this;
        },
        evtAdd = function () {
            var ce = this.element;
            ce.addEventListener('mousemove', this.selectionMousemove.bind(this), false);
            ce.addEventListener('mousedown', this.selectionMousedown.bind(this), false);
            ce.addEventListener('mouseup', this.selectionMouseup.bind(this), false);
            ce.addEventListener('mouseout', this.selectionMouseout.bind(this), false);
            ce.onselectstart = function () { return false; };
            return this;
        },
        evtRem = function () {
            var ce = this.element;
            ce.removeEventListener('mousemove', this.selectionMousemove);
            ce.removeEventListener('mousedown', this.selectionMousedown);
            ce.removeEventListener('mouseup', this.selectionMouseup);
            ce.removeEventListener('mouseout', this.selectionMouseout);
            return this;
        },
        mouseMove = function (e) {
            var cord = this.cordinates,
                ses = this.handels,
                ce = this.element,
                selectors = this.options.selection.selectors,
                ratio = this.options.selection.aspectRatio,
                rezsize = this.result_ops.size;

            //mouse position on canvas
            var iMouseX = e.pageX - this.getTotalOffsetLeft(e.srcElement),
                iMouseY = e.pageY - this.getTotalOffsetTop(e.srcElement);

            if (iMouseX < 0 || iMouseY < 0) {
                console.log("imx or imy of canvas!!!");
                return;
            }

            if (this.MOUSEDOWN) {
                var newcord = {};

                if (this.isDragAll) {
                    newcord.x = iMouseX - cord.mx;
                    newcord.y = iMouseY - cord.my;
                }
                if (this.isDrag) {
                    //0  1  2
                    //3  4  5
                    //6  7  8
                    if (ratio.lockRatio) {
                        if (this.isHow[4]) { newcord.x = iMouseX - cord.mx; newcord.y = iMouseY - cord.my; }

                        if (this.isHow[1]) { newcord.y = iMouseY; newcord.h = cord.h + cord.y - iMouseY; newcord.w = newcord.h * this.scalew; }
                        if (this.isHow[3]) { newcord.x = iMouseX; newcord.w = cord.w + cord.x - iMouseX; newcord.h = newcord.w * this.scaleh; }
                        if (this.isHow[5]) { newcord.w = iMouseX - cord.x; newcord.h = newcord.w * this.scaleh; }
                        if (this.isHow[7]) { newcord.h = iMouseY - cord.y; newcord.w = newcord.h * this.scalew; }

                        this.Canvas.writeText(JSON.stringify(cord, ['x', 'y', 'w', 'h', 'mx', 'my']), 0, 30);
                    } else {
                        if (this.isHow[0]) { newcord.x = iMouseX; newcord.y = iMouseY; newcord.w = cord.w + cord.x - iMouseX; newcord.h = cord.h + cord.y - iMouseY; }
                        if (this.isHow[2]) { newcord.y = iMouseY; newcord.w = iMouseX - cord.x; newcord.h = cord.h + cord.y - iMouseY; }
                        if (this.isHow[6]) { newcord.x = iMouseX; newcord.w = cord.w + cord.x - iMouseX; newcord.h = iMouseY - cord.y; }
                        if (this.isHow[8]) { newcord.w = iMouseX - cord.x; newcord.h = iMouseY - cord.y; }

                        if (this.isHow[4]) { newcord.x = iMouseX - cord.mx; newcord.y = iMouseY - cord.my; }

                        if (this.isHow[1]) { newcord.y = iMouseY; newcord.h = cord.h + cord.y - iMouseY; }
                        if (this.isHow[3]) { newcord.x = iMouseX; newcord.w = cord.w + cord.x - iMouseX; }
                        if (this.isHow[5]) { newcord.w = iMouseX - cord.x; }
                        if (this.isHow[7]) { newcord.h = iMouseY - cord.y; }
                    }
                }
                this.Canvas.draw();
                this.selectionCalc(newcord).selectorsCalc().selectionDraw();

                //this.Canvas.writeText(JSON.stringify(cord, ['x', 'y', 'w', 'h', 'mx', 'my']), 0, 10);

                cord.mx = iMouseX - cord.x;
                cord.my = iMouseY - cord.y;

                return this;
            }

            // handle on mouse hower
            var how = false;
            for (var i = 0; i < selectors.active_zone.length; i++) {
                var j = selectors.active_zone[i];
                if (
                    iMouseX > ses[j].x && iMouseX < ses[j].x + selectors.size &&
                    iMouseY > ses[j].y && iMouseY < ses[j].y + selectors.size
                    ) {
                    this.isHow[j] = true;
                    how = j;
                } else {
                    this.isHow[j] = false;
                }
            }

            if (how !== false) {
                ce.style.cursor = selectors.mouseover[how];
                return this;
            }

            ce.style.cursor = 'auto';
        },
        mouseDown = function (e) {
            if (e.which !== 1) { return; }
            this.MOUSEDOWN = true;

            var cord = this.cordinates,
                selectors = this.options.selection.selectors,
                iMouseX = e.pageX - this.getTotalOffsetLeft(e.srcElement),
                iMouseY = e.pageY - this.getTotalOffsetTop(e.srcElement);//Math.floor

            cord.mx = iMouseX - cord.x;
            cord.my = iMouseY - cord.y;

            if (iMouseX > cord.x && iMouseX < cord.x + cord.w &&
                iMouseY > cord.y && iMouseY < cord.y + cord.h) {
                for (var i = 0; i < selectors.active_zone.length; i++) {
                    var j = selectors.active_zone[i];
                    if (this.isHow[j]) {
                        this.isDrag = true;
                        return this;
                    }
                }

                this.isDragAll = true;
            }
        },
        mouseUp = function (e) {
            var cord = this.cordinates;

            this.MOUSEDOWN = false;
            this.isDragAll = false;
            this.isDrag = false;
            cord.mx = 0;
            cord.my = 0;
            //TODO optimise somehow
            //if (ops.buttons.preview.show === false && ops.buttons.save.show === false) {
            //	this.previewScene();
            //	//this.getResult();
            //}
        },
        mouseOut = function (e) {
            if (e.which !== 1) { return; }
            var cord = this.cordinates;

            this.MOUSEDOWN = false;
            this.isDragAll = false;
            this.isDrag = false;
            cord.mx = 0;
            cord.my = 0;
            //this.canvas.element.style.cursor = 'auto';
        },
        getSelB64 = function () {
            var rops = this.result_ops,
                can = new _Canvas({ attr: rops.size }).init(),
                cord = this.cordinates,
                scale = 1 / this.scale,
                b64;

            can.ctx.drawImage(this.image,
                cord.x * scale,
                cord.y * scale,
                cord.w * scale,
                cord.h * scale,
                0, 0, can.element.width, can.element.height
                );

            b64 = can.element.toDataURL(rops.type, rops.quality);
            return b64;
        },
        getSelCords = function () {
            return this.cordinates;
        };
        init = function (/*refrence to image control*/image, result_ops) {
            this.Canvas = new _Canvas(this.options, "SelectionCanvas").init().image(image).reMesure().draw();
            this.ctx = this.Canvas.ctx;
            this.element = this.Canvas.element;
            this.width = this.Canvas.width;
            this.height = this.Canvas.height;
            this.image = this.Canvas.Image.element;
            this.scale = this.Canvas.scale;

            this.result_ops = result_ops;

            var newcords = { x: 0, y: 0 },
                ratio = this.options.selection.aspectRatio,
                rezsize = this.result_ops.size;

            this.scalew = rezsize.width / rezsize.height;
            this.scaleh = rezsize.height / rezsize.width;

            if (ratio.lockRatio) {
                this.options.selection.selectors.active_zone = [1, 3, 4, 5, 7];

                if (rezsize.width > rezsize.height) {
                    newcords.w = (this.width * this.scaleh <= this.height) ? this.width : this.height * this.scalew;
                    newcords.h = newcords.w * this.scaleh;
                } else {
                    newcords.w = this.height * this.scalew;
                    newcords.h = this.height;
                }
            } else {
                newcords.w = this.width;
                newcords.h = this.height;
            }

            this.selectionCalc(newcords).selectorsCalc().selectionDraw().selectionAttachEvents();

            return this;
        };

        return function () {
            this.selectionCalc = calcSel;
            this.selectorsCalc = calcSelHnd;
            this.selectionDraw = selDraw;
            this.selectionAttachEvents = evtAdd;
            this.selectionRemoveEvents = evtRem;
            this.selectionMousemove = mouseMove;
            this.selectionMousedown = mouseDown;
            this.selectionMouseup = mouseUp;
            this.selectionMouseout = mouseOut;
            this.init = init;
            this.getSelectionB64 = getSelB64;
            this.getSelectionCords = getSelCords;
            return this;
        };
    })();
    var _SelectionCanvas = function (ops, name, logProps) {
        _Control.call(this, ops, name, logProps);
        this.Canvas = null;
        this.width = 0;
        this.height = 0;
        this.ctx = null;
        this.scale = 1;
        this.image = null;
        this.result_ops = null;
        this.cordinates = { x: 0, y: 0, w: 0, h: 0, mx: 0, my: 0 };
        this.handels = [{ x: 0, y: 0 }, { x: 0, y: 0 }, { x: 0, y: 0 },
                       { x: 0, y: 0 }, { x: 0, y: 0 }, { x: 0, y: 0 },
                       { x: 0, y: 0 }, { x: 0, y: 0 }, { x: 0, y: 0 }];
        this.isHow = [false, false, false, false, false, false, false, false];
        this.isDrag = false;
        this.isDragAll = false;
        this.MOUSEDOWN = false;
    };
    asControl.call(_SelectionCanvas.prototype);
    asSelection.call(_SelectionCanvas.prototype);

    var asScene = (function () {
        var
        get_img_b64 = function () {
            return this.Selection.getSelectionB64();
        },
        get_desc = function () {
            return this.Input.element.childNodes[1].value;
        },
        get_date = function () {
            return this.Input.element.childNodes[0].value;
        },
        get_crop = function () {
            var cord = this.Selection.cordinates;
            var scale = this.Selection.scale;
            var orgCord = {
                x: Math.floor(cord.x / scale),
                y: Math.floor(cord.y / scale),
                w: Math.floor(cord.w / scale),
                h: Math.floor(cord.h / scale)
            };
            return orgCord;
        },
        get_orgsize = function () {
            var size = {
                w: this.Image.element.width,
                h: this.Image.element.height
            };
            return size;
        },
        addTbox = function () {
            /*TODO: implement dynamic callback*/
            var btt = this.options.toolbox.buttons,

            onopen = btt.open.callback,
            onsave = btt.save.callback,
            onpreview = btt.preview.callback,
            onrotate = btt.rotate.callback;
            onclose = btt.close.callback;

            var tbox = new _Toolbox(this.options.toolbox, "SceneToolbox", onopen, onsave, onpreview, onrotate, onclose).init().addToolbox(this);
            this.appendChild(tbox);
            return this;
        },
        addSE = function () {
            var ops = this.options.scene;

            this.Container = new _Container(ops, "Scene_container").init();
            this.Selection = new _SelectionCanvas(ops.edit.controls.edit, "SelectionOnCanvas").init(this.Image, this.options.result);
            this.Editor = new _Container(ops.edit, "editor").init().appendChild(this.Selection);

            this.Container.appendChild(this.Editor);
            this.appendChild(this.Container);
            return this;
        },
        addSP = function () {
            var ops = this.options.scene.preview,
                img = new _Image(ops.controls.preview, "Preview").init(),
                b64 = this.Selection.getSelectionB64();

            img.fromBase64(b64);
            this.Preview = new _Container(ops, "canvas_container").init().appendChild(img);
            this.Container.appendChild(this.Preview);

            return this;
        },
        addSI = function () {
            var ops = this.options.scene.input;

            if (ops.show) {
                this.Input = new _Container(ops, "Input_container").init();

                for (var item in ops.controls) {
                    if (ops.controls[item].show) {
                        var ni = new _Control(ops.controls[item], item).createSelf();

                        this.Input.appendChild(ni);
                    }
                }

                this.appendChild(this.Input);
            }
            return this;
        },
        addImage = function (/*image control*/image) {
            this.Image = image;
            return this;
        },
        init = function () {
            this.createSelf();
            return this;
        };

        return function () {
            this.init = init;
            this.addSceneEdit = addSE;
            this.addScenePreview = addSP;
            this.addSceneInput = addSI;
            this.addToolbox = addTbox;
            this.addImage = addImage;
            this.getImageB64 = get_img_b64;
            this.getDescription = get_desc;
            this.getDate = get_date;
            this.getCropCord = get_crop;
            this.getOrginalSize = get_orgsize;
            return this;
        };
    })();
    var _Scene = function (options, name, logProps) {
        _Control.call(this, options, name, logProps);
        this.Image = null;
        this.Selection = null;
        this.Editor = null;
        this.Preview = null;
        this.Input = null;
        this.Container = null;
    };
    asControl.call(_Scene.prototype);
    asContainer.call(_Scene.prototype);
    asScene.call(_Scene.prototype);

    var asSceneManager = (function () {
        var
        get_results = function () {
            //TODO: implement getresults
            return this;
        },
        addScene = function (evt) {
            var files = evt.srcElement.files,
                me = this,
                ops = this.options.control,
                callback = this.options.container.toolbox.buttons.open.callback,
                afterLoad = function () {
                    var scene = new _Scene(ops, "Scene").init().addTitle(++me.COUNT).addToolbox().addImage(this).addSceneInput().addSceneEdit();
                    me.appendChild(scene);
                    me.Scenes.push(scene);
                };

            if (files.length) {
                for (var i = 0; i < files.length; i++) {
                    //console.log(files[i].name + " " + files[i].type + " " + (files[i].size / 1024).toFixed(0) + "kB");

                    var reader = new FileReader();
                    reader.readAsArrayBuffer(files[i]);
                    reader.onload = loadImage;

                    if (callback && typeof callback == "function") {
                        callback(files[i]);
                    }
                }
            }

            function loadImage(event) {
                var blob = new Blob([event.target.result]),
                    wrul = window.URL || window.webkitURL,
                    image = new _Image(null, "OriginalImage", ["width", "height"], afterLoad)
                        .init().processBlobUrl(wrul.createObjectURL(blob));
            }

            return this;
        },
        remScene = function () {
            //TODO: implement removeScene
        },
        init = function () {
            var el = document.querySelector(this.name);

            if (el && el.nodeName == "DIV") {
                this.element = el;
            } else if (el.nodeName == "INPUT") {
                el.setAttribute("style", "display:none;");
                var container = document.createElement("div");
                container.setAttribute("id", this.name + "_1");
                el.parentNode.appendChild(container);
                this.element = container;
            } else {
                return null;
            }

            var tbox = new _Toolbox(this.options.container.toolbox, "MainControlBox", this.addScene).init().addToolbox(this),
                cnt = new _Container(this.options.container, "MainContainer").init().addTitle().appendChild(tbox);
            //scn = new _Container(this.options.container.controls, "ControlsContainer").init();

            this.appendChild(cnt);//.appendChild(scn);
            //this.scenes = scn.element;
            return this;
        };

        return function () {
            this.init = init;
            this.addScene = addScene;
            this.removeScene = remScene;
            this.getAllResults = get_results;
            return this;
        };
    })();
    var SceneManager = function (element) {
        this.scenes = null;
        this.Scenes = [];
        this.COUNT = 0;
        _Control.call(this, new OPTIONS(), element);
    };
    asControl.call(SceneManager.prototype);
    asSceneManager.call(SceneManager.prototype);

    this.ImageCropTool = function (/*css selector*/element) {
        return new SceneManager(element);
    };
})(); { }