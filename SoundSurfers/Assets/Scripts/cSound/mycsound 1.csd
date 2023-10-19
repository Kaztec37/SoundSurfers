<Cabbage>
</Cabbage>
<CsoundSynthesizer>
<CsOptions>
</CsOptions>

<CsInstruments>
ksmps = 32
nchnls = 2
0dbfs = 1
giWave1 ftgen 1, 0, 4096, 10, 1


instr PLAYER_MOVE
    kSpeed chnget "speed"
    aNoise buzz 0.4, 100, 3, -1
    a2 oscili 1,440, giWave1
    chnset a2, "osciliation"
    outs aNoise*kSpeed, aNoise*kSpeed 
endin
</CsInstruments>
<CsScore>
;causes Csound to run for about 7000 years...
f0 z

i"PLAYER_MOVE" 0 z
;starts instrument 1 and runs it for a week
</CsScore>
</CsoundSynthesizer>
