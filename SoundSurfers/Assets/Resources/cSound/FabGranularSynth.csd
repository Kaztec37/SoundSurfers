<CsoundSynthesizer>
<CsOptions>
-o dac ;-iadc -m128
</CsOptions>
<CsInstruments>

sr = 44100
ksmps = 32
nchnls = 2
0dbfs = 1

giWFn ftgen 0,0,16384,20,3,1
giWave ftgen 0,0,2^10,10,1,1/2,1/4,1/8,1/16,1/32,1/64

instr FabBryanSynth
kCps    chnget  "red"
kGDur   chnget  "green"
kDens   chnget  "blue"
kGDur = 0.01 + kGDur ; initialisation to avoid perf error 0.0
;kFmd    transeg 0,1,0,0, 10,4,15, 10,-4,0
;kPmd = 7
kFrPow = 0
kPrPow = 0
;kCps = 100
kPhs = 0
kFmd = 3
kPmd transeg 0,1,0,0, 10,4,1,  10,-4,0
;kGDur = 0.08
;kDens = 200
iMaxOvr = 1000
kFn = giWave
aGrainOut grain3 kCps, kPhs, kFmd, kPmd, kGDur, kDens, iMaxOvr, kFn, giWFn, kFrPow, kPrPow
;aOutEnv linseg  0, p3 * 0.05, 1, 0.05, 0.95, 0.8, 0.95, 0.1, 0
;gaOut8 = aOut8; * 0.2 * aOutEnv
outs aGrainOut * 0.5, aGrainOut * 0.5
endin

</CsInstruments>
<CsScore>
f0 z
i"FabBryanSynth" 1 -1
</CsScore>
</CsoundSynthesizer>
