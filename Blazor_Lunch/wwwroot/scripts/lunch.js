console.log('順序1')
$(function () {
    console.log('順序3')
    console.log('順序4')
    //中心点横坐标
    var dotLeft = ($(".container").width() - $(".dot").width()) / 2;
    //中心点纵坐标
    var dotTop = ($(".container").height() - $(".dot").height()) / 2;
    //起始角度
    var stard = 0;
    //半径
    var radius = 200;
    //每一个BOX对应的角度;
    var avd = 360 / $(".box").length;
    //每一个BOX对应的弧度;
    var ahd = avd * Math.PI / 180;
    //设置圆的中心点的位置
    $(".dot").css({ "left": dotLeft, "top": dotTop });
    $(".box").each(function (index, element) {
        $(this).css({
            "left": Math.sin((ahd * index)) * radius + dotLeft,
            "top": Math.cos((ahd * index)) * radius + dotTop
        });
    });
})
console.log('順序2')

function hello() {
    alert("Hello Tang!!")
}
console.log('順序?')