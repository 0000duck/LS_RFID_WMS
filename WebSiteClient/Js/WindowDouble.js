
var zindex = 0;
var t = 60;
move = null; //移动标记

//定义“获得指定dom节点”的函数，因为其重用率高
function $(d) { return document.getElementById(d); }

//父窗口内按下鼠标时，得到相关的值
function Down(obj) {
    move = 1;
    obj.x = event.x; //鼠标起始位置
    obj.y = event.y;
    obj.l = obj.offsetParent.offsetLeft; //父元素当前位置
    obj.t = obj.offsetParent.offsetTop;
    obj.w = obj.offsetParent.offsetWidth;
    obj.h = obj.offsetParent.offsetHeight;
    obj.setCapture(); //得到鼠标
    obj.style.backgroundColor = "orange";
    $("comment1").style.borderColor = "orange";
    $("comment2").style.borderColor = "orange";
}

//标题栏托动窗口
function Remove(obj) {
    if (move != null) {
        obj.offsetParent.style.left = event.x - obj.x + obj.l; // 鼠标移过的位置 + 父元素当前位置
        obj.offsetParent.style.top = event.y - obj.y + obj.t;
        obj.style.backgroundColor = "orange";
        $("comment1").style.borderColor = "orange";
        $("comment2").style.borderColor = "orange";
    }
}

//放开鼠标时，清理不用的东西
function Up(obj) {
    move = null;
    obj.releaseCapture(); //释放鼠标
    obj.style.backgroundColor = "#165ea9";
    $("comment1").style.borderColor = "#165ea9";
    $("comment2").style.borderColor = "#165ea9";
}

//控制div逐渐显示
function change_show(obj, w1, h1) {
    var _this = obj;
    var w = w1;
    var h = h1;
    var height = 0;
    var width = 0;
    var i = 0;
    //            if (!_this.style.display || _this.style.display == "none") {
    _this.style.display = "block";
    _this.style.height = 0 + "px";
    _this.style.width = 0 + "px";
    _this.style.zIndex = ++zindex;
    var startTime = new Date().getTime(); //开始执行的时间

    var timer = setInterval(function() {
        _this.style.left = Math.ceil(w) + "px";
        _this.style.top = Math.ceil(h) + "px";
        _this.style.height = Math.ceil(height) + "px";
        _this.style.width = Math.ceil(width) + "px";
        _this.style.filter = "Alpha(Opacity=" + i + ")"; //透明度逐渐变大
        //                    t++;
        var newTime = new Date().getTime();
        var timestamp = newTime - startTime;
        var change = (Math.pow((timestamp / 800 - 1), 3) + 1); //根据运行时间得到基础变化量
        i = 100 * change;
        //                    height = height + 2 * t / 3;
        //                    width = width + t;
        w = w1 * change;
        h = h1 * change;
        height = 560 * change;
        width = 800 * change;

        if (change >= 1) {
            clearInterval(timer);
            _this.style.left = w1 + "px";
            _this.style.top = h1 + "px";
            _this.style.height = height + "px";
            _this.style.width = width + "px";
        }
    }, 1);
    //            }
}
//控制div逐渐消失
function change_hidden(obj, w1, h1) {
    var _this = obj;
    var w = w1;
    var h = h1;
    var height = 560;
    var width = 800;
    var i = 100;
    _this.style.zIndex = --zindex;

    var timer = setInterval(function() {
        _this.style.left = Math.ceil(w) + "px";
        _this.style.top = Math.ceil(h) + "px";
        _this.style.height = Math.ceil(height) + "px";
        _this.style.width = Math.ceil(width) + "px";
        _this.style.filter = "Alpha(Opacity=" + i + ")"; //透明度逐渐变小
        //                    t++;
        i = i - t / 8;
        height = height - 2 * t / 3;
        width = width - t;
        if (width <= 0) {
            clearInterval(timer);
            _this.style.left = w + "px";
            _this.style.top = h + "px";
            _this.style.height = "0px";
            _this.style.width = "0px";
            _this.style.display = "none";
        }
    }, 1);
}

function Open(objName, w, h) {
    var obj = $(objName);
    change_show(obj, w, h)
}
function Close(objName, w, h) {
    var obj = $(objName);
    change_hidden(obj, w, h);
}
function Show(divName) {
    var obj = $(divName);
    obj.style.display = "block";
}
function Hide(divName) {
    var obj = $(divName);
    obj.style.display = "none";
}
