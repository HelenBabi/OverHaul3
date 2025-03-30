//[c3 charts Javascript]

//Project:	CRMi - Responsive Admin Template
//Primary use:   Used only for the morris charts


$(function() {
    "use strict";




    var n = c3.generate({
        bindto: "#column-oriented",
        size: {
            height: 350
        },
        color: {
            pattern: ['#38649f', '#389f99', '#ee1044']
        },
        data: {
            columns: [
                [' داده 1', 30, 20, 50, 40, 60, 50],
                [' داده 2', 200, 130, 90, 240, 130, 220],
                [' داده 3', 300, 200, 160, 400, 250, 250]
            ]
        },
        grid: {
            y: {
                show: !0
            }
        }
    });




    var a = c3.generate({
        bindto: "#data-color",
        size: {
            height: 350
        },
        data: {
            columns: [
                [' داده 1', 30, 20, 50, 40, 60, 50],
                [' داده 2', 200, 130, 90, 240, 130, 220],
                [' داده 3', 300, 200, 160, 400, 250, 250]
            ],
            type: "bar",
            colors: {
                data1: "#38649f",
                data2: "#389f99"
            },
            color: function(a, o) {
                return o.id && "data3" === o.id ? d3.rgb(a).darker(o.value / 150) : a
            }
        },
        grid: {
            y: {
                show: !0
            }
        }
    });



    var a = c3.generate({
        bindto: "#data-order",
        size: {
            height: 350
        },
        color: {
            pattern: ["#2196f3", "#7f21f3", "#00bfa5", "#f32184", "#e2e023"]
        },
        data: {
            columns: [
                [' داده 1', 130, 200, 320, 400, 530, 750],
                [' داده 2', -130, 10, 130, 200, 150, 250],
                [' داده 3', -130, -50, -10, -200, -250, -150]
            ],
            type: "bar",
            groups: [
                [" داده 1", " داده 2", " داده 3"]
            ],
            order: "کاهشی"
        },
        grid: {
            x: {
                show: !0
            }
        }
    });
    setTimeout(function() {
        a.load({
            columns: [
                [' داده 4', 1200, 1300, 1450, 1600, 1520, 1820],
            ]
        })
    }, 1e3), setTimeout(function() {
        a.load({
            columns: [
                [' داده 5', 200, 300, 450, 600, 520, 820],
            ]
        })
    }, 2e3), setTimeout(function() {
        a.groups([
            [" داده 1", " داده 2", " داده 3", " داده 4", " داده 5"]
        ])
    }, 3e3)



    var o = c3.generate({
        bindto: "#row-oriented",
        size: {
            height: 350
        },
        color: {
            pattern: ['#38649f', '#389f99', '#ee1044']
        },
        data: {
            rows: [
                [' داده 1', ' داده 2', ' داده 3'],
                [90, 120, 300],
                [40, 160, 240],
                [50, 200, 290],
                [120, 160, 230],
                [80, 130, 300],
                [90, 220, 320],
            ]
        },
        grid: {
            y: {
                show: !0
            }
        }
    });




    var o = c3.generate({
        bindto: "#category-data",
        size: {
            height: 350
        },
        color: {
            pattern: ['#389f99', '#ee1044']
        },
        data: {
            x: "x",
            columns: [
                ['x', 'www.fudatco.com', 'www.fudatco.com', 'www.fudatco.com', 'www.fudatco.com'],
                ['دانلود', 30, 200, 100, 400],
                ['بارگذاری', 90, 100, 140, 200],
            ],
            groups: [
                ["دانلود", "بارگذاری"]
            ],
            type: "bar"
        },
        axis: {
            x: {
                type: "category"
            }
        },
        grid: {
            y: {
                show: !0
            }
        }
    });
    setTimeout(function() {
        o.load({
            columns: [
                ['x', 'www.fudatco1.com', 'www.fudatco11.com', 'www.fudatco12.com', 'www.fudatco13.com'],
                ['دانلود', 130, 200, 150, 350],
                ['بارگذاری', 190, 180, 190, 140],
            ]
        })
    }, 1e3), setTimeout(function() {
        o.load({
            columns: [
                ['x', 'www.fudatco3.com', 'www.fudatco31.com', 'www.fudatco32.com'],
                ['دانلود', 30, 300, 200],
                ['بارگذاری', 90, 130, 240],
            ]
        })
    }, 2e3), setTimeout(function() {
        o.load({
            columns: [
                ['x', 'www.fudatco2.com', 'www.fudatco21.com', 'www.fudatco22.com', 'www.fudatco23.com'],
                ['دانلود', 130, 300, 200, 470],
                ['بارگذاری', 190, 130, 240, 340],
            ]
        })
    }, 3e3), setTimeout(function() {
        o.load({
            columns: [
                ['دانلود', 30, 30, 20, 170],
                ['بارگذاری', 90, 30, 40, 40],
            ]
        })
    }, 4e3), setTimeout(function() {
        o.load({
            url: "../c3_string_x.csv"
        })
    }, 5e3);





});