<CsoundSynthesizer>
<CsOptions>
-odac -iadc -m128 ; Output to the default audio device with a 128-sample buffer
</CsOptions>
<CsInstruments>
sr = 44100 ; Sample rate: 44.1 kHz
ksmps = 32 ; Control rate: 32 samples per control period
nchnls = 2 ; Number of audio channels (stereo)
0dbfs = 1 ; Full-scale amplitude

; Create a table (giTable) for one second of recording
giTable ftgen 0, 0, sr, 2, 0

; Create another table (giHalfSine) for modulation
giHalfSine ftgen 0, 0, 1024, 9, 0.5, 1, 0

giDelay = 1 ; Delay time in milliseconds

; Instrument 'Record': Record audio input to the table
instr Record
  aIn = inch(1) ; Capture audio input from channel 1
  gaWritePointer = phasor(1) ; Generate a phasor for writing to the table
  tablew(aIn, gaWritePointer, giTable, 1) ; Write audio to giTable
endin

schedule("Record", 0, -1) ; Schedule 'Record' instrument to run continuously

; Instrument 'Granulator': Granulate the recorded audio
instr Granulator
  kGrainDur = 30 ; Grain duration in milliseconds
  kTranspos = -300 ; Transposition in cents
  kDensity = 50 ; Density of grains in Hz
  kDistribution = 0.5 ; Grain distribution (0-1)
  kTrig = metro(kDensity) ; Generate a trigger signal based on density
  
  if kTrig == 1 then
    kPointer = k(gaWritePointer) - giDelay / 1000 ; Calculate the pointer position
    kOffset = random:k(0, kDistribution / kDensity) ; Randomize the offset
    
    schedulek("Grain", kOffset, kGrainDur / 1000, kPointer, cent(kTranspos))
  endif
endin

schedule("Granulator", giDelay / 1000, -1) ; Schedule 'Granulator' instrument

; Instrument 'Grain': Generate granular synthesis
instr Grain
  iStart = p4 ; Start position
  iSpeed = p5 ; Speed (frequency)
  aOut = poscil3:a(poscil3:a(0.3, 1 / p3, giHalfSine), iSpeed, giTable, iStart)
  out(aOut, aOut) ; Output stereo signal
endin

</CsInstruments>
<CsScore>
</CsScore>
</CsoundSynthesizer>
; Example by Joachim Heintz
