
<CsoundSynthesizer>
<CsOptions>
-o dac
</CsOptions>
<CsInstruments>

sr = 44100
ksmps = 32
nchnls = 2
0dbfs = 1

instr FabSynth 
kRed chnget "red"
kGreen chnget "green"
kBlue chnget "blue"
aModulatorRed poscil 20,  kRed
aModulatorGreen poscil 20, kGreen
aModulatorBlue poscil 20, kBlue
aReModRed poscil 0.5, 250+aModulatorRed
aReModGreen poscil 0.5, 250+aModulatorGreen
aReModBlue poscil 0.5, 250+aModulatorBlue
aOurOutput = (aReModRed * aReModGreen * aReModBlue)
outs aOurOutput, aOurOutput
//outc aReModRed, aReModGreen, aReModBlue
;outc aModulatorRed, aModulatorGreen, aModulatorBlue

endin

</CsInstruments>
<CsScore>
f0 z
i "FabSynth" 1 -1
</CsScore>
</CsoundSynthesizer>


