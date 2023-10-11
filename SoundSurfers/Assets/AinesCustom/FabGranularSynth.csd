<CsoundSynthesizer>
<CsOptions>
-odac -iadc -m128
</CsOptions>
<CsInstruments>

sr = 44100
ksmps = 32
nchnls = 3
0dbfs = 1

giTable ftgen 0, 0, sr, 2, 0 ;for one second of recording
giHalfSine ftgen 0, 0, 1024, 9, .5, 1, 0
giDelay = 1 ;ms

instr Record
aRed chnget "red"
aGreen chnget "green"
aBlue chnget "blue"
gaWritePointer = phasor(1)
tablew(aRed, gaWritePointer, giTable, 1)
tablew(aGreen, gaWritePointer, giTable, 1)
tablew(aBlue, gaWritePointer, giTable, 1)
endin

schedule("Record",0,-1)

instr Granulator
kGrainDur = 30 ;milliseconds
kTranspos = -300 ;cent
kDensity = 50 ;Hz (grains per second)
kDistribution = .5 ;0-1
kTrig = metro(kDensity)
if kTrig==1 then
kPointer = k(gaWritePointer)-giDelay/1000
kOffset = random:k(0,kDistribution/kDensity)
schedulek("Grain",kOffset,kGrainDur/1000,kPointer,cent(kTranspos))
endif
endin

schedule("Granulator",giDelay/1000,-1)

instr Grain
iStart = p4
iSpeed = p5
aMod = poscil3:a(0.3, 1/p3, giHalfSine)
aOutR = poscil3:a(aMod, iSpeed, giTable, iStart)
aOutG = poscil3:a(aMod, iSpeed, giTable, iStart)
aOutB = poscil3:a(aMod, iSpeed, giTable, iStart)
out(aOutR, aOutG, aOutB)
endin

</CsInstruments>
<CsScore>
;f0 z
;i "Grain" 1 -1
</CsScore>
</CsoundSynthesizer>
