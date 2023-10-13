<CsoundSynthesizer>
<CsOptions>
; Select audio/midi flags here according to platform
; Audio out   Audio in    No messages
-odac          ;;;RT audio I/O

--nodisplays

</CsOptions>
<CsInstruments>

; Initialize the global variables.
sr = 48000 
kr = 480 
ksmps = 100 
nchnls = 2

; Set 0dbfs to 1
0dbfs = 1

giSine		ftgen	0, 0, 65537, 10, 1
giCosine	ftgen	0, 0, 8193, 9, 1, 1, 90

giWave  	ftgen  	0,0,2^10,10,1,1/2,1/4,1/8,1/16,1/32,1/64
giBuzz  	ftgen 	0,0,4096,11,40,1,0.9
;giSine		ftgen 	0,0,4096,10,1
giSineWave	ftgen	0,0,16384,10,1


giFMWave  	ftgen  	0, 0, 2^10, 10, 1, 1/2, 1/4, 1/8, 1/16, 1/32, 1/64
;giCosine	ftgen	0, 0, 8193, 9, 1, 1, 90		; cosine
giDisttab	ftgen	0, 0, 32768, 7, 0, 32768, 1	; for kdistribution
giFile		ftgen	0, 0, 0, 1, "24cellRow.wav", 0, 0, 0	; soundfile for source waveform
;giFile2		ftgen	0, 0, 0, 1, "8cellRow.wav", 0, 0, 0	; soundfile for fm waveform
;giFile3		ftgen	0, 0, 0, 1, "5cellRow.wav", 0, 0, 0	; source waveform
giWin		ftgen	0, 0, 4096, 20, 6, 1		; gaussian window 
giPan		ftgen	0, 0, 32768, -21, 1		; for panning (random values between 0 and 1)
giAttack	ftgen	0, 0, 513, 5, 0.0001, 512, 1	; exponential curve 
giDecay		ftgen	0, 0, 513, 5, 1, 512, 0.0001	; exponential curve
;giGainMask	ftgen	0, 0, 5, 2, 0, 3, 1, 0.9, 0
giWavFreqStart	ftgen	0, 0, 5, 2, 0, 2, 1, 0.4, 0
giWavFreqEnd	ftgen	0, 0, 5, 2, 0, 2, 1, 0.9, 0
giFmIndex	ftgen	0, 0, 5, 2, 0, 3, 0.8, 0.4, 0 
giFmEnv		ftgen	0, 0, 129, 7, 0, 32, 1, 12, 0.7, 64, 0.7, 20, 0
giWavAmp	ftgen	0, 0, 8, 2, 0, 4, 1, 1, 1, 1, 0 

;window function - used as an amplitude envelope for each grain
;(bartlett window)
giWFn   	ftgen 	0,0,16384,20,3,1

;**************************************************************************************
instr 2 ; Modal Instrument
;**************************************************************************************

; get control value from application
;kSineControlVal	chnget	"sineControlVal"

iamp    init ampdbfs(-24)

;kFreqScale chnget "randFreq" ; random frequency scale value sent from application
;kWgbowAmpVal chnget "randAmp"
;kWgbowPressureVal chnget "randPressure"
;kWgbowPosVal chnget "randPos"
kGaussRange	chnget	"gaussRange"
kModeFreq1	chnget 	"modeFreq1"
kModeFreq2	chnget	"modeFreq2"

kRangeMin	gauss	kGaussRange	
kRangeMin += 65
kRangeMax	gauss	kGaussRange
kRangeMax += 80	
kcpsMin		gauss	kGaussRange * 0.1
kcpsMin += 4
kcpsMax		gauss	kGaussRange * 0.1
kcpsMax += 6

kFreqScale	rspline	kRangeMin,	kRangeMax,	kcpsMin,	kcpsMax

kRangeMin2	gauss	kGaussRange * 0.01
kRangeMin2 += -21
kRangeMax2	gauss	kGaussRange * 0.01
kRangeMax2 += -15

kWgbowAmpVal	rspline	kRangeMin2,	kRangeMax2,	kcpsMin,	kcpsMax

kRangeMin3	gauss	kGaussRange * 0.05
kRangeMin3 += 2	
kRangeMax3	gauss	kGaussRange * 0.05
kRangeMax3 += 4	

kWgbowPressureVal	rspline	kRangeMin3,	kRangeMax3,	kcpsMin,	kcpsMax

kRangeMin4	gauss	kGaussRange * 0.0004
kRangeMin4 += 0.127236	
kRangeMax4	gauss	kGaussRange * 0.0004
kRangeMax4 += 0.15	

kWgbowPosVal	rspline	kRangeMin4,	kRangeMax4,	kcpsMin,	kcpsMax

kRangeMin5	gauss	kGaussRange * 0.05
kRangeMin5 += 2	
kRangeMax5	gauss	kGaussRange * 0.05
kRangeMax5 += 10	

kWgbowVibF	rspline	kRangeMin5,	kRangeMax5,	kcpsMin,	kcpsMax

kRangeMin6	gauss	kGaussRange * 0.01
kRangeMin6 += -21	
kRangeMax6	gauss	kGaussRange * 0.01
kRangeMax6 += -15	

kWgbowVibAmp	rspline	kRangeMin6,	kRangeMax6,	kcpsMin,	kcpsMax

; mallet excitator----------------------------------

; to simulate the shock between the excitator and the resonator
;krand	random	1,	10	
;ashock  mpulse ampdbfs(-1), krand,	2
;
;; felt excitator from mode.csd
;;aexc1	mode	ashock,	80 * (kFreqScale + 1.0),	8
;aexc1	mode	ashock,	80,	8
;aexc1 = aexc1 * iamp
;
;;aexc2	mode	ashock,	188 * (kFreqScale * 1.0),	3
;aexc2	mode	ashock,	188,	3
;aexc2 = aexc2 * iamp
;
;aexc	sum	aexc1,	aexc2

; bow excitator-------------------------------------

kamp = ampdbfs(kWgbowAmpVal)
kfreq = kFreqScale 
kpres = kWgbowPressureVal
krat = kWgbowPosVal 
kvibf = kWgbowVibF 
kvamp = ampdbfs(kWgbowVibAmp)

aexc	wgbow	kamp,	kfreq,	kpres,	krat,	kvibf,	kvamp

;"Contact" condition : when aexc reaches 0, the excitator looses 
;contact with the resonator, and stops "pushing it"
aexc limit	aexc,	0,	3*iamp 

; ratios from http://www.csounds.com/manual/html/MiscModalFreq.html
ares1	mode	aexc,	100 + kGaussRange,	220 + kGaussRange; * 0.8

ares2	mode	aexc,	142 + kGaussRange,	280 + kGaussRange; * 0.4

ares3	mode	aexc,	211 + kGaussRange,	200 + kGaussRange; * 0.2

ares4	mode	aexc,	247 + kGaussRange,	220 + kGaussRange; * 0.2

ares5	mode	aexc,	467.9,	140 + kGaussRange * (kModeFreq1 * 0.1)	

ares6	mode	aexc,	935.8, 	140 + kGaussRange * (kModeFreq2 * 0.1)	

ares	sum	ares1,	ares2,	ares3,	ares4,	ares5, ares6

;gaOut1 = (aexc + ares) * kSineControlVal 
aout2 = (aexc * 0.001) + (ares * 0.166)
gaOut2 = aout2 * 0.05
	;outs	gaOut2,	gaOut2

kRms	rms	gaOut2
	chnset	kRms,	"rmsOut"

endin

;**************************************************************************************
instr 6 ; Particle Note Scheduler
;**************************************************************************************

kFileSpeed	chnget	"fileSpeed"
;kFileSpeed = 5.0
kGrainDurFactor chnget	"grainSize"
;kGrainDurFactor = 90.0

kGaussVal 	gauss 	6.0
kGaussVal2	gauss	100

seed 0
kRand random 0.2, 10.8
kRand2 random 1, 20  

kTrigger metro  kRand2 
	 chnset kTrigger, "metroOut"

kMinTim		= 0 
kMaxNum 	= 1
kInsNum 	= 7
kWhen 		= 2
gkDur 		= kRand 
kSpeed 		= kFileSpeed + kGaussVal
kGrainFreq 	= p4 + kGaussVal
kGrainDurFactor = kGrainDurFactor + kGaussVal2
kCentCalc 	= kGrainFreq + kGaussVal
kPosRand 	= 100 + kGaussVal
kCentRand 	= kCentCalc + kGaussVal 
kPanCalc 	= 1
kDist 		= 0.7 

schedkwhen kTrigger, kMinTim, kMaxNum, kInsNum, kWhen, gkDur, kSpeed, kGrainFreq, kGrainDurFactor, kCentCalc, kPosRand, kCentRand, kPanCalc, kDist 

endin

; **********************************************************************************************
; uses the files "24cellRow.wav" "8cellRow.wav" & "5cellRow.wav" 
; original partikkel example by Joachim Heintz and Oeyvind Brandtsegg 2008
; **********************************************************************************************
instr 7 ;Particle Instrument
; **********************************************************************************************

/*score parameters*/
ispeed			= p4		; 1 = original speed 
igrainfreq		= p5		; grain rate
igraindurfactor 	= p6		; grain size in ms
icent 	 		= p7		; transposition in cent
iposrand		= p8		; time position randomness (offset) of the pointer in ms
icentrand		= p9		; transposition randomness in cents
ipan			= p10		; panning narrow (0) to wide (1)
idist			= p11		; grain distribution (0=periodic, 1=scattered)

kduration = (0.5/igrainfreq)*igraindurfactor

/*get length of source wave file, needed for both transposition and time pointer*/
ifilen			tableng	giFile
ifildur			= ifilen / sr

/*sync input (disabled)*/
async			= 0		

/*grain envelope*/
kenv2amt		= 0.5		; use only secondary envelope
ienv2tab 		= giWin		; grain (secondary) envelope
ienv_attack		= giAttack 		; default attack envelope 
ienv_decay		= giDecay 		; default decay envelope 
ksustain_amount		= 0.8		; sustain for fraction of grain length
ka_d_ratio		= 0.75 		; no meaning in this case (use only secondary envelope, ienv2tab)

/*amplitude*/
kamp			= ampdbfs(0)	; grain amplitude
igainmasks		= -1		; gain masking

/*transposition*/
kcentrand		rand icentrand	; random transposition
iorig			= 1 / ifildur	; original pitch
kwavfreq		= iorig; * cent(icent + kcentrand)

/*other pitch related (disabled)*/
ksweepshape		= 0;0.8		; no frequency sweep
iwavfreqstarttab 	= -1;giWavFreqStart		; frequency sweep start
iwavfreqendtab		= -1;giWavFreqEnd		; frequency sweep end

;aEnv	linseg	0, p3 * 0.2, 1, p3 * 0.1, 0.8, p3 * 0.5, 0.8, p3 * 0.2, 0
;kCps	linseg	200, p3 * 0.8, 500,  p3 * 0.2, 100
;aSig	oscili aEnv, kCps, giFMWave	

awavfm			= 0;aSig 
ifmamptab		= -1;giFmIndex		; FM scaling 
kfmenv			= -1;giFmEnv		; FM envelope 

/*trainlet related (disabled)*/
icosine			= giCosine	; cosine ftable
kTrainCps		= igrainfreq; set trainlet cps equal to grain rate for single-cycle trainlet in each grain
knumpartials		= 1		; number of partials in trainlet
kchroma			= 1		; balance of partials in trainlet

/*panning, using channel masks*/
imid			= .5; center
ileftmost		= imid - ipan/2
irightmost		= imid + ipan/2
giPanthis		ftgen	0, 0, 32768, -24, giPan, ileftmost, irightmost	; rescales giPan according to ipan
			tableiw		0, 0, giPanthis				; change index 0 ...
			tableiw		32766, 1, giPanthis			; ... and 1 for ichannelmasks
ichannelmasks		= giPanthis		; ftable for panning

/*random gain masking*/
krandommask		= 0.2	

/*source waveforms*/
kwaveform1		= giFile; source waveforms
kwaveform2		= giFile
kwaveform3		= giFile
kwaveform4		= giFile
iwaveamptab		= -1; mix of source waveforms and trainlets

/*time pointer*/
afilposphas		phasor ispeed / ifildur

/*generate random deviation of the time pointer*/
iposrandsec		= iposrand / 1000	; ms -> sec
iposrand		= iposrandsec / ifildur	; phase values (0-1)
krndpos			linrand	 iposrand	; random offset in phase values
;kGaussVal		gauss	20.0

/*add random deviation to the time pointer*/
asamplepos1		= afilposphas + krndpos; resulting phase values (0-1)
asamplepos2		= asamplepos1 + krndpos; + kGaussVal
asamplepos3		= asamplepos2 + krndpos; + kGaussVal	
asamplepos4		= asamplepos1 + krndpos; + kGaussVal	

/*original key for each source waveform*/
kwavekey1		= 1
kwavekey2		= 0.5 
kwavekey3		= 1.32 
kwavekey4		= 0.66 

/* maximum number of grains per k-period*/
imax_grains		= 3000		

aOut		partikkel igrainfreq, idist, giDisttab, async, kenv2amt, ienv2tab, \
		ienv_attack, ienv_decay, ksustain_amount, ka_d_ratio, kduration, kamp, igainmasks, \
		kwavfreq, ksweepshape, iwavfreqstarttab, iwavfreqendtab, awavfm, \
		ifmamptab, kfmenv, icosine, kTrainCps, knumpartials, \
		kchroma, ichannelmasks, krandommask, kwaveform1, kwaveform2, kwaveform3, kwaveform4, \
		iwaveamptab, asamplepos1, asamplepos2, asamplepos3, asamplepos4, \
		kwavekey1, kwavekey2, kwavekey3, kwavekey4, imax_grains

aOutEnv	linseg	0, p3 * 0.05, 1, p3 * 0.05, 0.85, p3 * 0.8, 0.85, p3 * 0.1, 0

gaParticleOut = aOut * aOutEnv

endin

;**************************************************************************************
instr 8 ; granular instrument using grain3
;**************************************************************************************

kCps	chnget	"grainFreq"
kPhs	chnget	"grainPhase"
kFmd	chnget	"randFreq"
kPmd	chnget	"randPhase"
kGDur	chnget	"grainDur"
kDens	chnget	"grainDensity"
kFrPow	chnget	"grainFreqVariationDistrib"
kPrPow	chnget	"grainPhaseVariationDistrib"
;kFn	chnget	"grainWaveform"

kGDur = 0.01 + kGDur ; initialisation to avoid perf error 0.0
kDens = 1 + kDens

; get control value from application
kSineControlVal	chnget	"sineControlVal"
;kCps = kCps * kSineControlVal + 20

  ;kCPS    =       100
  ;kPhs    =       0
  ;kFmd    transeg 0,1,0,0, 10,4,15, 10,-4,0
  ;kFmd	= 3
  ;kPmd    transeg 0,1,0,0, 10,4,1,  10,-4,0
  ;kPmd	= 7
  ;kGDur   =       0.08
  ;kDens   =       200
  iMaxOvr =       1000
  kFn     =       giWave
  ;print info. to the terminal
          ;printks "Random Phase:%5.2F%TPitch Random:%5.2F%n",1,kPmd,kFmd
	;printks "Grain Density:%f%n", 1, kDens
  aOut8    grain3  kCps, kPhs, kFmd, kPmd, kGDur, kDens, iMaxOvr, kFn, giWFn, kFrPow, kPrPow

aOutEnv	linseg	0, p3 * 0.05, 1, 0.05, 0.95, 0.8, 0.95, 0.1, 0

gaOut8 = aOut8; * 0.2 * aOutEnv

	;outs	gaOut8, gaOut8
endin

;**************************************************************************************
instr 9 ; Real-time Spectral Analysis Instrument 
;**************************************************************************************

ifftsize = 1024 
ioverlap = ifftsize / 4
iwinsize = ifftsize * 2
iwinshape = 0

aSig  = gaParticleOut 

; route output from instrument 2 above to pvsanal
fsig	pvsanal	aSig,	ifftsize,	ioverlap,	iwinsize,	iwinshape

kcent	pvscent	fsig
	chnset	kcent,	"specCentOut"

kFreq,	kAmp	pvspitch	fsig,	0.01
	chnset	kFreq,	"freqOut"
	chnset	kAmp,	"ampOut"

; get info from pvsanal and print
ioverlap,	inbins,	iwindowsize,	iformat	pvsinfo	fsig
print	ioverlap,	inbins,	iwindowsize,	iformat		

; create tables to write frequency data
iFreqTable	ftgen	0,	0,	inbins,	2,	0
iAmpTable	ftgen	0,	0,	inbins,	2,	0

; write frequency data to function table
kFlag	pvsftw	fsig,	iAmpTable,	iFreqTable	

 if kFlag == 0 goto contin 

;************** Frequency Processing *****************

; modify frequency data from fsig with mandelbulb escape values from application
kCount = 0

loop:

	; read amplitude data from iAmpTable
	kAmp	tablekt	kCount,	iAmpTable

	; send val out to application
	S_ChannelName	sprintfk	"fftAmpBin%d",	kCount
	chnset	kAmp,	S_ChannelName
	
	loop_lt	kCount,	1,	inbins,	loop

contin:

endin

;**************************************************************************************
instr 12 ; Sound Localisation Instrument
;**************************************************************************************
kPortTime linseg 0.0, 0.001, 0.05 

iNumAudioSources init 3

kAzimuths[] 	init 	iNumAudioSources
kElevations[] 	init	iNumAudioSources
kDistances[]	init	iNumAudioSources

kCount = 0

channelLoop:

	S_azimuth sprintfk "azimuth%d", kCount
	kAzimuths[kCount] 	chnget S_azimuth

	S_elevation sprintfk "elevation%d", kCount 
	kElevations[kCount] 	chnget S_elevation 

	S_distance sprintfk "distance%d", kCount
	kDistances[kCount]	chnget S_distance 

	loop_lt	kCount, 1, iNumAudioSources, channelLoop
	
aInstSigs[]	init	iNumAudioSources
aInstSigs[0] = gaOut2
aInstSigs[1] = gaParticleOut * 0.5
aInstSigs[2] = gaParticleOut * 0.5 

aLeftSigs[]	init	iNumAudioSources
aRightSigs[]	init	iNumAudioSources
kDistVals[]	init	iNumAudioSources

kDistVals[0] portk kDistances[0], kPortTime	;to filter out audio artifacts due to the distance changing too quickly
	
aLeftSigs[0], aRightSigs[0]  hrtfmove2	aInstSigs[0], kAzimuths[0], kElevations[0], "hrtf-48000-left.dat", "hrtf-48000-right.dat", 4, 9.0, 48000
aLeftSigs[0] = aLeftSigs[0] / (kDistVals[0] + 0.00001)
aRightSigs[0] = aRightSigs[0] / (kDistVals[0] + 0.00001)

kDistVals[1]	portk	kDistances[1],	kPortTime

aLeftSigs[1], aRightSigs[1]  hrtfmove2	aInstSigs[1], kAzimuths[1], kElevations[1], "hrtf-48000-left.dat", "hrtf-48000-right.dat", 4, 9.0, 48000
aLeftSigs[1] = aLeftSigs[1] / (kDistVals[1] + 0.00001)
aRightSigs[1] = aRightSigs[1] / (kDistVals[1] + 0.00001)

kDistVals[2] portk kDistances[2], kPortTime	;to filter out audio artifacts due to the distance changing too quickly
	
aLeftSigs[2], aRightSigs[2]  hrtfmove2	aInstSigs[2], kAzimuths[2], kElevations[2], "hrtf-48000-left.dat", "hrtf-48000-right.dat", 4, 9.0, 48000
aLeftSigs[2] = aLeftSigs[2] / (kDistVals[2] + 0.00001)
aRightSigs[2] = aRightSigs[2] / (kDistVals[2] + 0.00001)

aL init 0
aR init 0

aL sum aLeftSigs[0], aLeftSigs[1], aLeftSigs[2]
aR sum aRightSigs[0], aRightSigs[1], aRightSigs[2]

outs	aL,	aR
endin

</CsInstruments>
<CsScore>

;********************************************************************
; f tables
;********************************************************************
;p1	p2	p3	p4	p5	p6	p7	p8	p9	p10	p11	p12	p13	p14	p15	p16	p17	p18	p19	p20	p21	p22	p23	p24	p25

f0	86400 ;keep csound running for a day

f1	0	1025	8	0	2	1	3	0	4	1	6	0	10	1	12	0	16	1	32	0	1	0	939	0

;********************************************************************
; score events
;********************************************************************

i2	2	-1

i6.01	1	-1	0
i6.02	2	-1	50.0	

;i7	2	-1

;i8	2	-1

i9.01	1	-1
i9.02	2	-1

i12	2	-1
e
</CsScore>
</CsoundSynthesizer>
