move = null; //移动标记
wmin = 100; //最小的窗口为100x100
zmax = 10000; //刻录当前的最高层
ssize = 6; //阴影宽度

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
    //Shadow(obj) //重画阴影
    $('shadow').style.visibility = 'hidden'; //隐藏阴影
    obj.setCapture(); //得到鼠标
    obj.style.backgroundColor = "orange";
    $('window').style.borderColor = "orange";
}

//标题栏托动窗口
function Remove(obj) {
    if (move != null) {
        obj.offsetParent.style.left = event.x - obj.x + obj.l; // 鼠标移过的位置 + 父元素当前位置
        obj.offsetParent.style.top = event.y - obj.y + obj.t;
        obj.style.backgroundColor = "orange";
        $('window').style.borderColor = "orange";
        $('window').style.filter = "Alpha(Opacity=50)"; 
        $('window').style.opacity = 0.5; //兼容FireFox
        //obj.nextSibling.style.color = "orange";
       // Shadow(obj) //重画阴影
    }
}

//状态栏改变窗口大小
function Resize(obj) {
    if (move != null) {
        obj.offsetParent.style.width = Math.max(wmin, event.x - obj.x + obj.w); //窗口的width, height不能为负数
        obj.offsetParent.style.height = Math.max(wmin, event.y - obj.y + obj.h);
        Shadow(obj) //重画阴影
    }
}

//放开鼠标时，清理不用的东西
function Up(obj) {
    move = null;
    // $('shadow').style.visibility = 'hidden'; //隐藏阴影
    Shadow($("windowtitle"));
    obj.releaseCapture(); //释放鼠标
    obj.style.backgroundColor = "#165ea9";
    $('window').style.borderColor = "#165ea9";
    $('window').style.filter = "Alpha(Opacity=100)"; //透明度逐渐变小
    $('window').style.opacity = 1; //兼容FireFox
}

//父窗口按下鼠标时，将当前层置顶
function Focus(obj) {
    zmax = zmax + 10; //最高层（变量）每次点击加10，以保证最高
    obj.style.zIndex = zmax; //将当前层置顶，当前层 = 最高层
    $('shadow').style.zIndex = zmax - 1; //阴影的层 = 最高层 - 1
}

//标题栏按下鼠标或移动窗口时，重画阴影
function Shadow(obj) {
    /**
    * 阴影的位置 = 新的父元素位置 + 阴影宽度
    */
    $('shadow').style.left = obj.offsetParent.offsetLeft + ssize;
    $('shadow').style.top = obj.offsetParent.offsetTop + ssize;
    $('shadow').style.width = obj.offsetParent.offsetWidth;
    $('shadow').style.height = obj.offsetParent.offsetHeight;
    $('shadow').style.visibility = 'visible';
}

//控制div逐渐显示
var i = 0;
function change_show() {
    var obj = $("window");
    i = i + 10; //逐渐显示速度
    obj.style.filter = "Alpha(Opacity=" + i + ")"; //透明度逐渐变小
    obj.style.opacity = i / 100; //兼容FireFox
    if (i > 70) {
        Focus($("window"));
        Shadow($("windowtitle"));
    }
    if (i >= 100) {
        clearInterval(s);
        i = 0;
    }
    $("window").style.display = "block";
}

//控制div逐渐消失
var j = 100;
function change_hidden() {
    var obj = $("window");
    j = j - 10; //逐渐消失速度
    obj.style.filter = "Alpha(Opacity=" + j + ")"; //透明度逐渐变大
    obj.style.opacity = j / 100; //兼容FireFox
    if (j <= 0) {
        clearInterval(h);
        obj.style.display = "none";
        j = 100;
    }
    $('shadow').style.visibility = 'hidden';
}

//控制change_show()行为
var s;
function Open() {
    if (s) { clearInterval(s); }
    s = setInterval(change_show, 1);
}

//控制change_hidden()行为
function Close() {
    h = setInterval(change_hidden, 1);
}