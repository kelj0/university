package com.iamu.kkeglje.ruler;

import android.app.Activity;
import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.os.Build;
import android.util.AttributeSet;
import android.util.DisplayMetrics;
import android.view.KeyEvent;
import android.view.View;

import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;

public class DrawView extends View {
    Paint paint = new Paint();
    DisplayMetrics dm = new DisplayMetrics();
    float duljinaMilimetra;
    int height,width;

    private void init(){
        paint.setColor(Color.BLACK);
        paint.setStrokeWidth(2.25f);
        paint.setTextSize(20f);
    }

    public DrawView(Context context) {
        super(context);
        init();
    }

    public DrawView(Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
        init();
    }

    public DrawView(Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
        init();
    }

    @RequiresApi(api = Build.VERSION_CODES.LOLLIPOP)
    public DrawView(Context context, @Nullable AttributeSet attrs, int defStyleAttr, int defStyleRes) {
        super(context, attrs, defStyleAttr, defStyleRes);
        init();
    }

    @Override
    protected void onDraw(Canvas canvas) {
        ((Activity) getContext()).getWindowManager()
                .getDefaultDisplay()
                .getMetrics(dm);
        this.duljinaMilimetra = (float)(1.0f * this.dm.xdpi / 25.4);
        this.height = this.dm.heightPixels;
        this.width = this.dm.widthPixels;
        int txt = 0;
        int mmCounter = 0;
        System.out.println(this.duljinaMilimetra);
        for(int i = 20; i < this.width-20;i+=(int)this.duljinaMilimetra){
            if(mmCounter%10 == 0 || mmCounter == 0) {
                canvas.drawLine(i, 10, i, 100, this.paint);
                canvas.drawText(String.valueOf(txt++),i-8,120,paint);
            }else{
                canvas.drawLine(i, 40, i, 70, this.paint);
            }
            ++mmCounter;
        }

    }
}
