$(function () {
    //交替显示行
    $('#alternation').click(function () {
        $('tbody > tr:odd', $('#example')).toggleClass('alternation');
    });

    //三色交替显示行
    $('#alternationThree').click(function () {
        $('tbody > tr:nth-child(3n)', $('#example')).toggleClass('alternation');
        $('tbody > tr:nth-child(3n+2)', $('#example')).toggleClass('alternation3');
    });

    //选择行
    $('#selectTr').click(function () {
        //为表格行添加选择事件处理
        $('tbody > tr', $('#example')).click(function () {
            $('.selected').removeClass('selected');
            $(this).addClass('selected'); //this 表示引发事件的对象，但它不是 jquery 对象
        }).hover(		//注意这里的链式调用
            function () {
                $(this).addClass('mouseOver');
            },
            function () {
                $(this).removeClass('mouseOver');
            }
        );
    });

    //增加排序功能
    $('#sort').click(tableSort);

    //获取排好序后的主键值
    $('#getSequence').click(function () {
        var sequence = [];
        $('#content input[name=noticeSelect]').each(function () {
            sequence.push(this.value);
        });
        alert(sequence.join(','));
    });

    //获取表格中已选择的复选框的值集合
    $('#getSelected').click(function () {
        var sequence = [];
        $('#content input[name=noticeSelect]:checked').each(function () {
            sequence.push(this.value);
        });
        alert(sequence.join(','));
    });

    //按日期降序排列
    $('#dateDesc').click(descByDate);
});