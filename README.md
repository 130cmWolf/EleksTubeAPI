# Eleks Tube Set Time

## API Protocol v6.2

Not sure...

> API v5.1  
> https://forum.eleksmaker.com/topic/1941/elekstube-api-control-protocol

### Link Serial Baud Rate
```
115200
```

### Link Real Time Color & Time Protocol

```
/(header)1(first tube number)66CCFF(Standard Sixteen Binary RGB color)166CCFF(second tube number and color).....

eg. /166CCFF266CCFF1FFFFFF2FFFFFF1FFFFFF2FFFFFF //Tube will show 12:12:12 and color is blue blue white white white white
 
This is only for real time tube exhibition
```


### Save Time Color & Time Protocol

```
*(header)1(first tube number)66CCFF(Standard Sixteen Binary RGB color)166CCFF(second tube number and color).....

eg.   *166CCFF266CCFF1FFFFFF2FFFFFF1FFFFFF2FFFFFF //Tube will save time and color 12:12:12 and color is blue blue white white white white

This is only for real time tube exhibition
```
### Mode Change and Switch AM/PM

```
$(header)000

$0 no support

$01 is Monochrome mode
$02 is Flow mode
$03 is Breathing mode
$04 is Rainbow mode
$05 is TimeLine mode
$06 is TestMode mode

$000 is for 24H mode
$001 is for AM/PM mode
```
