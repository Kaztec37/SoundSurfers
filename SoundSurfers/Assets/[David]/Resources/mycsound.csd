<CsoundSynthesizer>
<CsOptions>
</CsOptions>

<CsInstruments>
instr PLAYER_MOVE
    kSpeed chnget "speed"
    aNoise buzz 0.4, 100, 3, -1
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
