
{
    // helper functions
    const MathUtils = {
        // map number x from range [a, b] to [c, d]
        map: (x, a, b, c, d) => (x - a) * (d - c) / (b - a) + c,
        // linear interpolation
        lerp: (a, b, n) => (1 - n) * a + n * b,
        // Random float
        getRandomFloat: (min, max) => (Math.random() * (max - min) + min).toFixed(2)
    };

    // body element
    const body = document.body;

    // calculate the viewport size
    let winsize;
    const calcWinsize = () => winsize = { width: window.innerWidth, height: window.innerHeight };
    calcWinsize();
    // and recalculate on resize
    window.addEventListener('resize', calcWinsize);

    // scroll position
    let docScroll;

    // for scroll speed calculation
    let lastScroll;
    let scrollingSpeed = 0;
    // scroll position update function
    var getPageYScroll = () => docScroll = window.pageYOffset || document.documentElement.scrollTop;
    window.addEventListener('scroll', getPageYScroll);

    // Item
    class Item {
        constructor(el) {
            // the .item element
            this.DOM = { el: el };
            // the inner image
            this.DOM.image = this.DOM.el.querySelector('.content__item-img');
            this.DOM.imageWrapper = this.DOM.image.parentNode;
            this.DOM.title = this.DOM.el.querySelector('.content__item-title');
            this.renderedStyles = {
                // here we define which property will change as we scroll the page and the item is inside the viewport
                // in this case we will be:
                // - scaling the inner image
                // - translating the item's title
                // we interpolate between the previous and current value to achieve a smooth effect
                imageScale: {
                    // interpolated value
                    previous: 0,
                    // current value
                    current: 0,
                    // amount to interpolate
                    ease: 0.1,
                    // current value setter
                    setValue: () => {
                        const toValue = 1.5;
                        const fromValue = 1;
                        const val = MathUtils.map(this.props.top - docScroll, winsize.height, -1 * this.props.height, fromValue, toValue);
                        return Math.max(Math.min(val, toValue), fromValue);
                    }
                },
                titleTranslationY: {
                    previous: 0,
                    current: 0,
                    ease: 0.1,
                    fromValue: Number(MathUtils.getRandomFloat(30, 400)),
                    setValue: () => {
                        const fromValue = this.renderedStyles.titleTranslationY.fromValue;
                        const toValue = -1 * fromValue;
                        const val = MathUtils.map(this.props.top - docScroll, winsize.height, -1 * this.props.height, fromValue, toValue);
                        return fromValue < 0 ? Math.min(Math.max(val, fromValue), toValue) : Math.max(Math.min(val, fromValue), toValue);
                    }
                }
            };
            // gets the item's height and top (relative to the document)
            this.getSize();
            // set the initial values
            this.update();
            // use the IntersectionObserver API to check when the element is inside the viewport
            // only then the element styles will be updated
            this.observer = new IntersectionObserver((entries) => {
                entries.forEach(entry => this.isVisible = entry.intersectionRatio > 0);
            });
            this.observer.observe(this.DOM.el);
            // init/bind events
            this.initEvents();
        }
        update() {
            // sets the initial value (no interpolation)
            for (const key in this.renderedStyles) {
                this.renderedStyles[key].current = this.renderedStyles[key].previous = this.renderedStyles[key].setValue();
            }
            // apply changes/styles
            this.layout();
        }
        getSize() {
            const rect = this.DOM.el.getBoundingClientRect();
            this.props = {
                // item's height
                height: rect.height,
                // offset top relative to the document
                top: docScroll + rect.top
            }
        }
        initEvents() {
            window.addEventListener('resize', () => this.resize());
        }
        resize() {
            // gets the item's height and top (relative to the document)
            this.getSize();
            // on resize reset sizes and update styles
            this.update();
        }
        render() {
            // update the current and interpolated values
            for (const key in this.renderedStyles) {
                this.renderedStyles[key].current = this.renderedStyles[key].setValue();
                this.renderedStyles[key].previous = MathUtils.lerp(this.renderedStyles[key].previous, this.renderedStyles[key].current, this.renderedStyles[key].ease);
            }

            // and apply changes
            this.layout();
        }
        layout() {
            // scale the image
            this.DOM.image.style.transform = `scale3d(${this.renderedStyles.imageScale.previous},${this.renderedStyles.imageScale.previous},1)`;
            // translate the title
            this.DOM.title.style.transform = `translate3d(0,${this.renderedStyles.titleTranslationY.previous}px,0)`;
        }
    }

    // SmoothScroll
    class SmoothScroll {
        constructor() {
            // the <main> element
            this.DOM = { main: document.querySelector('main') };
            // the scrollable element
            // we translate this element when scrolling (y-axis)
            this.DOM.scrollable = this.DOM.main.querySelector('div[data-scroll]');
            // the items on the page
            this.items = [];
            this.DOM.content = this.DOM.main.querySelector('.content');
            [...this.DOM.content.querySelectorAll('.content__item')].forEach(item => this.items.push(new Item(item)));
            // here we define which property will change as we scroll the page
            // in this case we will be translating on the y-axis
            // we interpolate between the previous and current value to achieve the smooth scrolling effect
            this.renderedStyles = {
                translationY: {
                    // interpolated value
                    previous: 0,
                    // current value
                    current: 0,
                    // amount to interpolate
                    ease: 0.1,
                    // current value setter
                    // in this case the value of the translation will be the same like the document scroll
                    setValue: () => docScroll
                }
            };
            // set the body's height
            this.setSize();
            // set the initial values
            this.update();
            // the <main> element's style needs to be modified
            this.style();
            // init/bind events
            this.initEvents();
            // start the render loop
            requestAnimationFrame(() => this.render());
        }
        update() {
            // sets the initial value (no interpolation) - translate the scroll value
            for (const key in this.renderedStyles) {
                this.renderedStyles[key].current = this.renderedStyles[key].previous = this.renderedStyles[key].setValue();
            }
            // translate the scrollable element
            this.layout();
        }
        layout() {
            this.DOM.scrollable.style.transform = `translate3d(0,${-1 * this.renderedStyles.translationY.previous}px,0)`;
        }
        setSize() {
            // set the heigh of the body in order to keep the scrollbar on the page
            body.style.height = `${this.DOM.scrollable.scrollHeight}px`;
        }
        style() {
            // the <main> needs to "stick" to the screen and not scroll
            // for that we set it to position fixed and overflow hidden 
            this.DOM.main.style.position = 'fixed';
            this.DOM.main.style.width = this.DOM.main.style.height = '100%';
            this.DOM.main.style.top = this.DOM.main.style.left = 0;
            this.DOM.main.style.overflow = 'hidden';
        }
        initEvents() {
            // on resize reset the body's height
            window.addEventListener('resize', () => this.setSize());
        }
        render() {
            // Get scrolling speed
            // Update lastScroll
            scrollingSpeed = Math.abs(docScroll - lastScroll);
            lastScroll = docScroll;

            // update the current and interpolated values
            for (const key in this.renderedStyles) {
                this.renderedStyles[key].current = this.renderedStyles[key].setValue();
                this.renderedStyles[key].previous = MathUtils.lerp(this.renderedStyles[key].previous, this.renderedStyles[key].current, this.renderedStyles[key].ease);
            }
            // and translate the scrollable element
            this.layout();

            // for every item
            for (const item of this.items) {
                // if the item is inside the viewport call it's render function
                // this will update item's styles, based on the document scroll value and the item's position on the viewport
                if (item.isVisible) {
                    if (item.insideViewport) {
                        item.render();
                    }
                    else {
                        item.insideViewport = true;
                        item.update();
                    }
                }
                else {
                    item.insideViewport = false;
                }
            }
            rendNew();
            // loop..
            requestAnimationFrame(() => this.render());
        }
    }

    /***********************************/
    /********** Preload stuff **********/

    // Preload images
    const preloadImages = () => {
        return new Promise((resolve, reject) => {
            imagesLoaded(document.querySelectorAll('.content__item-img'), { background: true }, resolve);
        });
    };

    // And then..
    preloadImages().then(() => {
        // Remove the loader
        document.body.classList.remove('loading');
        // Get the scroll position and update the lastScroll variable
        getPageYScroll();

        lastScroll = docScroll;

        // Initialize the Smooth Scrolling
        new SmoothScroll();
    });


    //AJAX本地处理搜索
    var ajax = {
        request: (method, url, callback) => {
            var req = new XMLHttpRequest();
            req.onreadystatechange = function () {
                if (req.readyState == 4 && req.status == 200) {
                    callback(req.responseText);
                }
            };

            req.open(method, url, true);
            req.setRequestHeader("Access-Control-Allow-Origin", "*");
            req.setRequestHeader("Access-Control-Allow-Headers", "X-Requested-With");
            req.setRequestHeader("Access-Control-Allow-Methods", "PUT,POST,GET,DELETE,OPTIONS");
            req.send();

        }

    }
    // var dynamicL = (anyFunction) => {
    //     anyFunction(docScroll);
    // }
    // dynamicL(() => {
    //     alert(getPageYScroll());
    //     if (getPageYScroll() < 0) {
    //         alert("cool2");
    //     }
    // });

    //对获取数组的构造
    var BackItems = { 'urls': [], 'getUrls': function () { return this.urls } };
    //请求量的控制
    var RequestVolume = {
        'requestNum': 5, 'loadedNum': 0, 'refreshNum': function () {
            this.loadedNum += this.requestNum
        }, 'loadMoreThumbnail': function () {

            for (i = this.loadedNum; i < this.loadedNum + this.requestNum; i++) {
                if (i >= BackItems.getUrls().length) {
                    break;
                } else {
                    //alert("num" + i)
                    // insertToHtml("insert_thumbnail", "<a id=\"" + i.toString() + "picc\" class=\"tm-social-link\"><img src=\"http://192.168.10.7/assets/Materials/M_Atlas/M_Atlas_Plant/M_Atlas_Plant_Trees_1045_Preview.png\" class=\"tm-social-link\" loading=\"lazy\"></img><div>" + BackItems.getUrls()[i] + "</div></a>")
                    //---
                    var a = document.createElement('a');
                    a.id = String(i) + "picc";
                    a.className = "tm-social-link";
                    a.textContent = BackItems.getUrls()[i];
                    a.onclick = function name(params) {

                    }
                    //ToDo---
                    var img = document.createElement('img');
                    img.src = "http://192.168.10.7/assets/Materials/M_Atlas/M_Atlas_Plant/M_Atlas_Plant_Trees_1045_Preview.png";
                    img.className = "tm-social-link";
                    img.loading = "lazy";
                    //---
                    //---
                    document.getElementById("insert_thumbnail").appendChild(a);
                    a.parentNode.insertBefore(img, a);
                    document.getElementById(String(i) + "picc").appendChild(img);

                }

            }
        }
    }



    ajax.request("GET", "http://127.0.0.1:8848/PicShow/aa.json", (text) => {
        var data = eval('(' + text + ')');
        var testGet = data.items;
        var data2 = JSON.stringify(testGet);
        var obj = JSON.parse(data2);
        var len = Object.keys(testGet).length;
        for (i = 0; i < len; i++) {

            BackItems.urls.push(obj[i].name);
        }


    }
    );

    function insertToHtml(Id, Code) {

        document.getElementById(Id).innerHTML += Code;


    }
    function rendNew() {

        console.log((docScroll / 700 % 1).toFixed(2));

        if ((docScroll / 700 % 1).toFixed(2) == 0.99) {
            RequestVolume.loadMoreThumbnail();
            RequestVolume.refreshNum();
        }

    }
    //跳转到指定位置
    document.getElementById("1pic").onclick = function () {




        //alert(RequestVolume.loadedNum);
    };


    document.getElementById("2pic").onclick = function () {

        //docScroll=1000;
        docScroll = 200 + 800 * 1



    };
    document.getElementById("3pic").onclick = function () {

        docScroll = 200 + 800 * 2;

    };
    document.getElementById("4pic").onclick = function () {

        docScroll = 200 + 800 * 3;

    };
    document.getElementById("5pic").onclick = function () {

        docScroll = 200 + 800 * 4;

    };
    document.getElementById("6pic").onclick = function () {

        docScroll = 200 + 800 * 5;

    };

}
