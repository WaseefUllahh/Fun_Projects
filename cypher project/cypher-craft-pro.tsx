import React, { useState, useEffect, useRef } from 'react';
import { Award, Trophy, Lock, Unlock, Download, Plus, Target } from 'lucide-react';

const CypherCraftPro = () => {
  const [activeTab, setActiveTab] = useState('encrypt');
  const [category, setCategory] = useState('substitution');
  const [algorithm, setAlgorithm] = useState('caesar');
  const [plainText, setPlainText] = useState('');
  const [settings, setSettings] = useState({
    caesarShift: 3,
    vigenereKey: '',
    railfenceRails: 3,
    playfairKey: '',
    columnarKey: '',
    simpleblockKey: '',
    xorKey: '',
    aesKey: '',
    dhPrivateKey: 5
  });
  
  const [animationState, setAnimationState] = useState({
    isActive: false,
    isAuto: false,
    steps: [],
    currentStep: 0,
    plainDisplay: [],
    cipherDisplay: [],
    visualContent: '',
    explanation: { beginner: '', intermediate: '', advanced: '' }
  });

  const [progress, setProgress] = useState({
    completedCiphers: [],
    completedChallenges: [],
    totalAttempts: 0,
    rank: 'Rookie'
  });

  const [challenges, setChallenges] = useState([
    { id: 1, title: 'Caesar Basics', cipher: 'caesar', plaintext: 'HELLO', shift: 3, locked: false, completed: false },
    { id: 2, title: 'Vigenère Master', cipher: 'vigenere', plaintext: 'CRYPTOGRAPHY', keyword: 'KEY', locked: false, completed: false },
    { id: 3, title: 'XOR Challenge', cipher: 'xor', plaintext: 'SECRET', locked: false, completed: false },
    { id: 4, title: 'AES Champion', cipher: 'aes', plaintext: 'TOPSECRET', locked: true, completed: false },
    { id: 5, title: 'Diffie-Hellman Exchange', cipher: 'diffiehellman', locked: true, completed: false }
  ]);

  const [customChallenge, setCustomChallenge] = useState({ title: '', cipher: 'caesar', plaintext: '' });
  const [showCertificate, setShowCertificate] = useState(false);

  const ALPHABET = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';

  const algorithms = {
    substitution: [
      { value: 'caesar', text: 'Caesar Cipher' },
      { value: 'atbash', text: 'Atbash Cipher' },
      { value: 'rot13', text: 'ROT13 Cipher' },
      { value: 'vigenere', text: 'Vigenère Cipher' }
    ],
    transposition: [
      { value: 'railfence', text: 'Rail Fence Cipher' }
    ],
    symmetric: [
      { value: 'xor', text: 'Simple XOR Cipher' },
      { value: 'aes', text: '🔒 AES (Simplified)', premium: true }
    ],
    asymmetric: [
      { value: 'rsa', text: 'Simplified RSA' },
      { value: 'diffiehellman', text: '🔒 Diffie-Hellman', premium: true }
    ],
    modern: [
      { value: 'blowfish', text: '🔒 Blowfish (Simplified)', premium: true }
    ]
  };

  useEffect(() => {
    updateAlgorithmOptions();
  }, [category]);

  useEffect(() => {
    if (animationState.isAuto && animationState.isActive && animationState.currentStep < animationState.steps.length) {
      const timer = setTimeout(() => {
        executeStep(animationState.currentStep);
      }, 1500);
      return () => clearTimeout(timer);
    }
  }, [animationState.isAuto, animationState.isActive, animationState.currentStep]);

  useEffect(() => {
    const completed = progress.completedCiphers.length;
    let rank = 'Rookie';
    if (completed >= 10) rank = 'Crypto Master';
    else if (completed >= 7) rank = 'Expert';
    else if (completed >= 5) rank = 'Advanced';
    else if (completed >= 3) rank = 'Intermediate';
    
    setProgress(prev => ({ ...prev, rank }));
  }, [progress.completedCiphers]);

  const updateAlgorithmOptions = () => {
    const algos = algorithms[category] || [];
    if (algos.length > 0) {
      setAlgorithm(algos[0].value);
    }
  };

  const preprocessText = (text, algo) => {
    if (['rsa', 'xor', 'aes', 'blowfish'].includes(algo)) return text.replace(/[^\x00-\x7F]/g, "");
    let upper = text.toUpperCase().replace(/[^A-Z]/g, '');
    
    if (algo === 'aes' || algo === 'blowfish') {
      const blockSize = algo === 'aes' ? 16 : 8;
      const len = upper.length;
      const remainder = len % blockSize;
      if (remainder !== 0) {
        upper += 'X'.repeat(blockSize - remainder);
      }
      return upper;
    }
    
    return upper;
  };

  const startEncryption = (isAuto) => {
    const text = plainText;
    
    if (algorithm === 'vigenere' && !settings.vigenereKey.match(/^[A-Za-z]+$/)) {
      alert('Vigenère requires an alphabet-only keyword!');
      return;
    }
    if (algorithm === 'aes' && settings.aesKey.length < 4) {
      alert('AES requires a key of at least 4 characters!');
      return;
    }
    if (algorithm === 'xor' && !settings.xorKey) {
      alert('XOR requires a key character!');
      return;
    }
    if (!text) {
      alert('Please enter some text to encrypt!');
      return;
    }

    const processedText = preprocessText(text, algorithm);
    const steps = generateSteps(processedText, algorithm);
    
    setAnimationState({
      isActive: true,
      isAuto,
      steps,
      currentStep: 0,
      plainDisplay: processedText.split(''),
      cipherDisplay: Array(processedText.length).fill('?'),
      visualContent: '',
      explanation: { beginner: '', intermediate: '', advanced: '' }
    });

    if (!progress.completedCiphers.includes(algorithm)) {
      setProgress(prev => ({
        ...prev,
        completedCiphers: [...prev.completedCiphers, algorithm],
        totalAttempts: prev.totalAttempts + 1
      }));
    } else {
      setProgress(prev => ({ ...prev, totalAttempts: prev.totalAttempts + 1 }));
    }
  };

  const executeStep = (stepIndex) => {
    if (stepIndex >= animationState.steps.length) {
      setAnimationState(prev => ({ ...prev, isActive: false }));
      return;
    }

    const step = animationState.steps[stepIndex];
    step();
    
    setAnimationState(prev => ({ ...prev, currentStep: stepIndex + 1 }));
  };

  const resetAnimation = () => {
    setAnimationState({
      isActive: false,
      isAuto: false,
      steps: [],
      currentStep: 0,
      plainDisplay: [],
      cipherDisplay: [],
      visualContent: '',
      explanation: { beginner: '', intermediate: '', advanced: '' }
    });
  };

  const generateSteps = (text, algo) => {
    const steps = [];
    
    const updateDisplay = (index, encryptedChar) => {
      setAnimationState(prev => ({
        ...prev,
        cipherDisplay: prev.cipherDisplay.map((c, i) => i === index ? encryptedChar : c)
      }));
    };

    const updateExplanation = (b, i, a) => {
      setAnimationState(prev => ({
        ...prev,
        explanation: { beginner: b, intermediate: i, advanced: a }
      }));
    };

    const updateVisual = (content) => {
      setAnimationState(prev => ({
        ...prev,
        visualContent: content
      }));
    };

    // AES Implementation
    if (algo === 'aes') {
      const key = settings.aesKey.toUpperCase();
      const S_BOX = {
        'A': 'N', 'B': 'O', 'C': 'P', 'D': 'Q', 'E': 'R', 'F': 'S', 'G': 'T', 'H': 'U',
        'I': 'V', 'J': 'W', 'K': 'X', 'L': 'Y', 'M': 'Z', 'N': 'A', 'O': 'B', 'P': 'C',
        'Q': 'D', 'R': 'E', 'S': 'F', 'T': 'G', 'U': 'H', 'V': 'I', 'W': 'J', 'X': 'K',
        'Y': 'L', 'Z': 'M'
      };

      steps.push(() => {
        updateExplanation(
          'AES encrypts data in 16-byte blocks using multiple rounds of substitution and permutation.',
          'AES (Advanced Encryption Standard) is a symmetric cipher used worldwide. This is a simplified educational version.',
          'AES uses SubBytes, ShiftRows, MixColumns, and AddRoundKey operations.'
        );
        updateVisual('<div class="text-center text-2xl text-yellow-300">🔒 AES Encryption Starting...</div>');
      });

      for (let i = 0; i < text.length; i += 16) {
        const block = text.substring(i, i + 16);
        
        steps.push(() => {
          updateExplanation(
            'Processing a 16-character block of data.',
            `AES works on 128-bit (16-byte) blocks. Current block: "${block}"`,
            `BLOCK[${i / 16}] = "${block}"`
          );
          updateVisual(`<div class="grid grid-cols-4 gap-2">${block.split('').map(c => `<div class="bg-blue-900 p-3 text-center rounded">${c}</div>`).join('')}</div>`);
        });

        const subBlock = block.split('').map(c => S_BOX[c] || c).join('');
        steps.push(() => {
          updateExplanation(
            'Each byte is replaced using a special substitution table (S-Box).',
            'The SubBytes step provides non-linearity to the cipher.',
            `S_BOX("${block}") → "${subBlock}"`
          );
          updateVisual(`<div class="grid grid-cols-4 gap-2">${subBlock.split('').map(c => `<div class="bg-green-900 p-3 text-center rounded">${c}</div>`).join('')}</div>`);
        });

        let finalBlock = '';
        for (let j = 0; j < subBlock.length; j++) {
          const keyChar = key[j % key.length];
          const charIndex = ALPHABET.indexOf(subBlock[j]);
          const keyIndex = ALPHABET.indexOf(keyChar);
          const newIndex = (charIndex + keyIndex) % 26;
          finalBlock += ALPHABET[newIndex];
        }

        steps.push(() => {
          updateExplanation(
            'Finally, we mix in our secret key to produce the encrypted block.',
            'The AddRoundKey step XORs the state with the round key.',
            `AddRoundKey: "${subBlock}" ⊕ key → "${finalBlock}"`
          );
          updateVisual(`<div class="grid grid-cols-4 gap-2">${finalBlock.split('').map(c => `<div class="bg-purple-900 p-3 text-center rounded">${c}</div>`).join('')}</div>`);
          
          for (let j = 0; j < finalBlock.length; j++) {
            updateDisplay(i + j, finalBlock[j]);
          }
        });
      }
      
      return steps;
    }

    // Blowfish Implementation
    if (algo === 'blowfish') {
      const P_ARRAY = [7, 3, 1, 5, 2, 6, 0, 4];
      
      steps.push(() => {
        updateExplanation(
          'Blowfish is a fast, secure cipher that encrypts data in 8-byte blocks.',
          'Blowfish uses a Feistel network with 16 rounds. This is a simplified educational version.',
          'Blowfish: P-array and S-boxes derived from π'
        );
        updateVisual('<div class="text-center text-2xl text-cyan-300">🐡 Blowfish Encryption</div>');
      });

      for (let i = 0; i < text.length; i += 8) {
        const block = text.substring(i, i + 8);
        
        steps.push(() => {
          updateExplanation(
            'Processing an 8-character block through the Feistel network.',
            `Blowfish block size is 64 bits (8 bytes). Block: "${block}"`,
            `BLOCK[${i / 8}] = "${block}"`
          );
          updateVisual(`<div class="grid grid-cols-4 gap-2">${block.split('').map(c => `<div class="bg-cyan-900 p-3 text-center rounded">${c}</div>`).join('')}</div>`);
        });

        let encrypted = block.split('').map((c, idx) => {
          const charIdx = ALPHABET.indexOf(c);
          if (charIdx === -1) return c;
          return ALPHABET[(charIdx + P_ARRAY[idx % 8]) % 26];
        }).join('');

        steps.push(() => {
          updateExplanation(
            'The Feistel function mixes the data with the key.',
            'Each round applies substitution and permutation operations.',
            `F(block) → "${encrypted}"`
          );
          updateVisual(`<div class="grid grid-cols-4 gap-2">${encrypted.split('').map(c => `<div class="bg-green-900 p-3 text-center rounded">${c}</div>`).join('')}</div>`);
          
          for (let j = 0; j < encrypted.length; j++) {
            updateDisplay(i + j, encrypted[j]);
          }
        });
      }
      
      return steps;
    }

    // Diffie-Hellman
    if (algo === 'diffiehellman') {
      const p = 23;
      const g = 5;
      const alicePrivate = settings.dhPrivateKey;
      const bobPrivate = 7;
      
      const alicePublic = Math.pow(g, alicePrivate) % p;
      const bobPublic = Math.pow(g, bobPrivate) % p;
      
      const aliceShared = Math.pow(bobPublic, alicePrivate) % p;

      steps.push(() => {
        updateExplanation(
          'Diffie-Hellman lets two people create a shared secret over an insecure channel!',
          'This allows secure key exchange without transmitting the key directly.',
          `Public parameters: p=${p} (prime), g=${g} (generator)`
        );
        updateVisual('<div class="text-center"><div class="text-3xl mb-4">👥 Key Exchange</div><div class="text-xl">Alice ←→ Bob</div></div>');
      });

      steps.push(() => {
        updateExplanation(
          `Alice picks a secret number (${alicePrivate}) and Bob picks one too (${bobPrivate}).`,
          'Each party generates a private key independently.',
          `Alice private: ${alicePrivate}, Bob private: ${bobPrivate}`
        );
        updateVisual(`<div class="grid grid-cols-2 gap-8"><div class="text-center p-4 bg-pink-900 rounded"><div class="text-2xl mb-2">👩 Alice</div><div>Secret: ${alicePrivate}</div></div><div class="text-center p-4 bg-blue-900 rounded"><div class="text-2xl mb-2">👨 Bob</div><div>Secret: ${bobPrivate}</div></div></div>`);
      });

      steps.push(() => {
        updateExplanation(
          'They each calculate a public number using a special formula and share it.',
          'Public keys are computed as g^private mod p',
          `Alice public: ${g}^${alicePrivate} mod ${p} = ${alicePublic}`
        );
        updateVisual(`<div class="text-center"><div class="mb-4">Alice sends: <span class="text-2xl text-yellow-300">${alicePublic}</span></div><div class="mb-4">Bob sends: <span class="text-2xl text-yellow-300">${bobPublic}</span></div><div class="text-sm text-gray-400">Anyone can see these numbers!</div></div>`);
      });

      steps.push(() => {
        updateExplanation(
          'Using their own secret and the other person\'s public number, they both arrive at the same shared secret!',
          'Each computes the shared secret using the received public key.',
          `Alice: ${bobPublic}^${alicePrivate} mod ${p} = ${aliceShared}`
        );
        updateVisual(`<div class="text-center"><div class="text-3xl mb-4">🔐</div><div class="text-2xl text-green-400">Shared Secret: ${aliceShared}</div><div class="text-sm mt-4 text-gray-400">Both Alice and Bob now have the same secret key!</div></div>`);
      });

      return steps;
    }

    // Character-by-character ciphers
    for (let i = 0; i < text.length; i++) {
      const char = text[i];
      const charIndex = ALPHABET.indexOf(char);

      steps.push(() => {
        updateExplanation(
          `Encrypting character '${char}'`,
          `Processing character at index ${i}`,
          `char[${i}] = '${char}'`
        );
      });

      if (charIndex === -1 && !['rsa', 'xor'].includes(algo)) {
        steps.push(() => {
          updateDisplay(i, char);
          updateExplanation(
            `Character '${char}' is not a letter, keeping it as is.`,
            'Non-alphabetic characters pass through unchanged.',
            'SKIP non-alpha'
          );
        });
        continue;
      }

      switch (algo) {
        case 'caesar':
        case 'rot13':
          const shift = algo === 'rot13' ? 13 : settings.caesarShift;
          const newIndex = (charIndex + shift) % 26;
          const encryptedChar = ALPHABET[newIndex];
          
          steps.push(() => {
            updateDisplay(i, encryptedChar);
            updateExplanation(
              `Shift '${char}' forward by ${shift} → '${encryptedChar}'`,
              `Position ${charIndex} + ${shift} = ${newIndex} → '${encryptedChar}'`,
              `(${charIndex} + ${shift}) % 26 = ${newIndex}`
            );
            updateVisual(`<div class="text-center text-4xl">${char} → ${encryptedChar}</div>`);
          });
          break;

        case 'atbash':
          const atbashChar = ALPHABET[25 - charIndex];
          steps.push(() => {
            updateDisplay(i, atbashChar);
            updateExplanation(
              `'${char}' mirrors to '${atbashChar}'`,
              `Reverse alphabet: position ${charIndex} → ${25 - charIndex}`,
              `ALPHABET[25 - ${charIndex}] = '${atbashChar}'`
            );
            updateVisual(`<div class="text-center text-4xl">${char} ↔️ ${atbashChar}</div>`);
          });
          break;

        case 'vigenere':
          const keyword = settings.vigenereKey.toUpperCase();
          const keyChar = keyword[i % keyword.length];
          const vShift = ALPHABET.indexOf(keyChar);
          const vNewIndex = (charIndex + vShift) % 26;
          const vEncryptedChar = ALPHABET[vNewIndex];
          
          steps.push(() => {
            updateDisplay(i, vEncryptedChar);
            updateExplanation(
              `Key letter '${keyChar}' shifts '${char}' by ${vShift} → '${vEncryptedChar}'`,
              `Using keyword '${keyword}', char ${i % keyword.length}`,
              `shift = ${vShift}; (${charIndex} + ${vShift}) % 26 = ${vNewIndex}`
            );
            updateVisual(`<div class="text-center"><div class="text-2xl mb-2">Key: ${keyChar}</div><div class="text-4xl">${char} → ${vEncryptedChar}</div></div>`);
          });
          break;

        case 'xor':
          const xorKey = settings.xorKey;
          const charCode = char.charCodeAt(0);
          const keyCode = xorKey.charCodeAt(0);
          const encryptedCode = charCode ^ keyCode;
          const xorEncrypted = String.fromCharCode(encryptedCode);
          
          steps.push(() => {
            updateDisplay(i, xorEncrypted);
            updateExplanation(
              `XOR '${char}' with key → '${xorEncrypted}'`,
              `${charCode} ⊕ ${keyCode} = ${encryptedCode}`,
              `${charCode.toString(2)} XOR ${keyCode.toString(2)} = ${encryptedCode.toString(2)}`
            );
            updateVisual(`<div class="text-center text-4xl">${char} ⊕ ${xorKey} = ${xorEncrypted}</div>`);
          });
          break;

        case 'rsa':
          const rsaN = 143, rsaE = 7;
          const rsaCharCode = char.charCodeAt(0);
          const rsaEncrypted = Number(BigInt(rsaCharCode) ** BigInt(rsaE) % BigInt(rsaN));
          const rsaChar = String.fromCharCode(rsaEncrypted);
          
          steps.push(() => {
            updateDisplay(i, rsaChar);
            updateExplanation(
              `Encrypt '${char}' using RSA → '${rsaChar}'`,
              `C = ${rsaCharCode}^${rsaE} mod ${rsaN} = ${rsaEncrypted}`,
              `Public key: (${rsaE}, ${rsaN})`
            );
            updateVisual(`<div class="text-center"><div class="text-xl mb-2">${rsaCharCode}^${rsaE} mod ${rsaN}</div><div class="text-4xl">= ${rsaEncrypted}</div></div>`);
          });
          break;
      }
    }

    return steps;
  };

  const completeChallenge = (challengeId) => {
    setChallenges(prev => prev.map(c => 
      c.id === challengeId ? { ...c, completed: true } : c
    ));
    setProgress(prev => ({
      ...prev,
      completedChallenges: [...new Set([...prev.completedChallenges, challengeId])]
    }));
  };

  const addCustomChallenge = () => {
    if (!customChallenge.title || !customChallenge.plaintext) {
      alert('Please fill in challenge title and plaintext!');
      return;
    }
    
    const newChallenge = {
      id: challenges.length + 1,
      title: customChallenge.title,
      cipher: customChallenge.cipher,
      plaintext: customChallenge.plaintext,
      locked: false,
      completed: false,
      custom: true
    };
    
    setChallenges(prev => [...prev, newChallenge]);
    setCustomChallenge({ title: '', cipher: 'caesar', plaintext: '' });
    alert('Challenge added successfully!');
  };

  const generateCertificate = () => {
    setShowCertificate(true);
  };

  const downloadCertificate = () => {
    alert('Certificate download feature! In a production app, this would generate a downloadable image.');
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-900 via-purple-900 to-gray-900 text-gray-100 p-4">
      <div className="max-w-7xl mx-auto">
        <div className="bg-gray-800 rounded-xl p-6 mb-6 border-2 border-purple-500 shadow-2xl">
          <div className="flex items-center justify-between flex-wrap gap-4">
            <div>
              <h1 className="text-4xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-yellow-400 to-purple-400">
                Cypher Craft Pro
              </h1>
              <p className="text-gray-400 mt-1">Premium Encryption Playground</p>
            </div>
            <div className="text-right">
              <div className="bg-yellow-400 text-gray-900 px-4 py-2 rounded-full font-bold text-sm mb-2">
                Rank: {progress.rank}
              </div>
              <div className="text-sm text-gray-400">
                {progress.completedCiphers.length} Ciphers Mastered
              </div>
            </div>
          </div>
        </div>

        <div className="flex gap-2 mb-6 flex-wrap">
          {['encrypt', 'challenges', 'progress'].map(tab => (
            <button
              key={tab}
              onClick={() => setActiveTab(tab)}
              className={`px-6 py-3 rounded-lg font-bold transition-all ${
                activeTab === tab
                  ? 'bg-purple-600 text-white shadow-lg'
                  : 'bg-gray-700 text-gray-300 hover:bg-gray-600'
              }`}
            >
              {tab === 'encrypt' && <Lock className="inline mr-2" size={16} />}
              {tab === 'challenges' && <Target className="inline mr-2" size={16} />}
              {tab === 'progress' && <Trophy className="inline mr-2" size={16} />}
              {tab.charAt(0).toUpperCase() + tab.slice(1)}
            </button>
          ))}
        </div>

        {activeTab === 'encrypt' && (
          <div className="bg-gray-800 rounded-xl p-6 border-2 border-gray-700">
            {!animationState.isActive ? (
              <>
                <div className="grid md:grid-cols-2 gap-6 mb-6">
                  <div>
                    <label className="block mb-2 font-bold text-yellow-400">Cipher Category</label>
                    <select
                      value={category}
                      onChange={(e) => setCategory(e.target.value)}
                      className="w-full p-3 rounded-lg bg-gray-900 border-2 border-gray-600 focus:border-purple-500 text-white"
                    >
                      <option value="substitution">Substitution Ciphers</option>
                      <option value="transposition">Transposition Ciphers</option>
                      <option value="symmetric">Symmetric Encryption</option>
                      <option value="asymmetric">Asymmetric Encryption</option>
                      <option value="modern">🔒 Modern Ciphers (Pro)</option>
                    </select>
                  </div>
                  <div>
                    <label className="block mb-2 font-bold text-yellow-400">Algorithm</label>
                    <select
                      value={algorithm}
                      onChange={(e) => setAlgorithm(e.target.value)}
                      className="w-full p-3 rounded-lg bg-gray-900 border-2 border-gray-600 focus:border-purple-500 text-white"
                    >
                      {(algorithms[category] || []).map(algo => (
                        <option key={algo.value} value={algo.value}>
                          {algo.text}
                        </option>
                      ))}
                    </select>
                  </div>
                </div>

                <div className="mb-6">
                  {algorithm === 'caesar' && (
                    <div>
                      <label className="block mb-2 font-bold text-yellow-400">
                        Shift Value: <span className="text-white">{settings.caesarShift}</span>
                      </label>
                      <input
                        type="range"
                        min="1"
                        max="25"
                        value={settings.caesarShift}
                        onChange={(e) => setSettings({...settings, caesarShift: parseInt(e.target.value)})}
                        className="w-full"
                      />
                    </div>
                  )}
                  
                  {algorithm === 'vigenere' && (
                    <div>
                      <label className="block mb-2 font-bold text-yellow-400">Keyword:</label>
                      <input
                        type="text"
                        value={settings.vigenereKey}
                        onChange={(e) => setSettings({...settings, vigenereKey: e.target.value})}
                        placeholder="e.g., LEMON"
                        className="w-full p-3 rounded-lg bg-gray-900 border-2 border-gray-600 focus:border-purple-500 text-white"
                      />
                    </div>
                  )}
                  
                  {algorithm === 'aes' && (
                    <div>
                      <label className="block mb-2 font-bold text-yellow-400">AES Key (min 4 chars):</label>
                      <input
                        type="text"
                        value={settings.aesKey}
                        onChange={(e) => setSettings({...settings, aesKey: e.target.value})}
                        placeholder="e.g., SECRET"
                        className="w-full p-3 rounded-lg bg-gray-900 border-2 border-gray-600 focus:border-purple-500 text-white"
                      />
                    </div>
                  )}
                  
                  {algorithm === 'xor' && (
                    <div>
                      <label className="block mb-2 font-bold text-yellow-400">XOR Key (single char):</label>
                      <input
                        type="text"
                        maxLength="1"
                        value={settings.xorKey}
                        onChange={(e) => setSettings({...settings, xorKey: e.target.value})}
                        placeholder="e.g., K"
                        className="w-full p-3 rounded-lg bg-gray-900 border-2 border-gray-600 focus:border-purple-500 text-white"
                      />
                    </div>
                  )}
                  
                  {algorithm === 'diffiehellman' && (
                    <div>
                      <label className="block mb-2 font-bold text-yellow-400">
                        Your Private Key: <span className="text-white">{settings.dhPrivateKey}</span>
                      </label>
                      <input
                        type="range"
                        min="2"
                        max="20"
                        value={settings.dhPrivateKey}
                        onChange={(e) => setSettings({...settings, dhPrivateKey: parseInt(e.target.value)})}
                        className="w-full"
                      />
                    </div>
                  )}
                </div>

                <div className="mb-6">
                  <label className="block mb-2 font-bold text-yellow-400">Your Message</label>
                  <textarea
                    value={plainText}
                    onChange={(e) => setPlainText(e.target.value)}
                    rows="4"
                    placeholder="Enter your top secret message..."
                    className="w-full p-3 rounded-lg bg-gray-900 border-2 border-gray-600 focus:border-purple-500 resize-none text-white"
                  />
                </div>

                <div className="grid md:grid-cols-2 gap-4">
                  <button
                    onClick={() => startEncryption(true)}
                    className="bg-gradient-to-r from-purple-600 to-pink-600 text-white py-4 px-6 rounded-lg font-bold hover:shadow-xl transition-all"
                  >
                    🚀 Auto Encrypt
                  </button>
                  <button
                    onClick={() => startEncryption(false)}
                    className="bg-gray-700 text-white py-4 px-6 rounded-lg font-bold hover:bg-gray-600 transition-all"
                  >
                    👆 Step-by-Step
                  </button>
                </div>
              </>
            ) : (
              <>
                <div className="mb-6">
                  <h3 className="text-2xl font-bold text-yellow-400 mb-4 text-center">
                    {algorithms[category]?.find(a => a.value === algorithm)?.text}
                  </h3>
                  
                  <div className="flex flex-wrap gap-2 justify-center mb-6">
                    {animationState.plainDisplay.map((char, i) => (
                      <div key={i} className="w-12 h-14 border-2 border-gray-600 rounded flex items-center justify-center text-xl font-bold">
                        {char}
                      </div>
                    ))}
                  </div>

                  <div className="bg-gray-900 rounded-lg p-6 mb-6 min-h-[200px] flex items-center justify-center">
                    <div dangerouslySetInnerHTML={{ __html: animationState.visualContent }} />
                  </div>

                  <div className="flex flex-wrap gap-2 justify-center mb-6">
                    {animationState.cipherDisplay.map((char, i) => (
                      <div key={i} className="w-12 h-14 border-2 border-green-600 rounded flex items-center justify-center text-xl font-bold text-green-400">
                        {char}
                      </div>
                    ))}
                  </div>

                  <div className="bg-gray-900 rounded-lg p-6 border-l-4 border-yellow-400">
                    <div className="mb-4">
                      <strong className="text-yellow-400">Beginner:</strong>
                      <div className="mt-1">{animationState.explanation.beginner}</div>
                    </div>
                    <div className="mb-4 text-cyan-300">
                      <strong>Intermediate:</strong>
                      <div className="mt-1">{animationState.explanation.intermediate}</div>
                    </div>
                    <div className="text-gray-400 font-mono text-sm">
                      <strong>Advanced:</strong>
                      <div className="mt-1">{animationState.explanation.advanced}</div>
                    </div>
                  </div>
                </div>

                <div className="grid md:grid-cols-2 gap-4">
                  {!animationState.isAuto && (
                    <button
                      onClick={() => executeStep(animationState.currentStep)}
                      disabled={animationState.currentStep >= animationState.steps.length}
                      className="bg-purple-600 text-white py-4 px-6 rounded-lg font-bold hover:bg-purple-700 transition-all disabled:opacity-50"
                    >
                      Next Step →
                    </button>
                  )}
                  <button
                    onClick={resetAnimation}
                    className="bg-red-600 text-white py-4 px-6 rounded-lg font-bold hover:bg-red-700 transition-all"
                  >
                    Reset
                  </button>
                </div>
              </>
            )}
          </div>
        )}

        {activeTab === 'challenges' && (
          <div className="space-y-6">
            <div className="bg-gray-800 rounded-xl p-6 border-2 border-gray-700">
              <h2 className="text-2xl font-bold text-yellow-400 mb-4 flex items-center">
                <Target className="mr-2" /> Cipher Challenges
              </h2>
              <div className="grid gap-4">
                {challenges.map(challenge => (
                  <div
                    key={challenge.id}
                    className={`p-4 rounded-lg border-2 ${
                      challenge.completed
                        ? 'bg-green-900 border-green-500'
                        : challenge.locked
                        ? 'bg-gray-900 border-gray-600 opacity-50'
                        : 'bg-gray-700 border-purple-500'
                    }`}
                  >
                    <div className="flex items-center justify-between flex-wrap gap-4">
                      <div>
                        <h3 className="text-xl font-bold flex items-center">
                          {challenge.locked ? <Lock size={20} className="mr-2" /> : <Unlock size={20} className="mr-2" />}
                          {challenge.title}
                        </h3>
                        <p className="text-gray-400 text-sm mt-1">
                          Cipher: {challenge.cipher} {challenge.plaintext && `| Text: "${challenge.plaintext}"`}
                        </p>
                      </div>
                      <div>
                        {challenge.completed ? (
                          <Award className="text-yellow-400" size={32} />
                        ) : challenge.locked ? (
                          <span className="text-gray-500 text-sm">Complete more to unlock</span>
                        ) : (
                          <button
                            onClick={() => {
                              setPlainText(challenge.plaintext || '');
                              setAlgorithm(challenge.cipher);
                              setActiveTab('encrypt');
                              setTimeout(() => completeChallenge(challenge.id), 5000);
                            }}
                            className="bg-purple-600 text-white px-4 py-2 rounded-lg hover:bg-purple-700 transition-all"
                          >
                            Start Challenge
                          </button>
                        )}
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            </div>

            <div className="bg-gray-800 rounded-xl p-6 border-2 border-gray-700">
              <h2 className="text-2xl font-bold text-yellow-400 mb-4 flex items-center">
                <Plus className="mr-2" /> Create Custom Challenge
              </h2>
              <div className="space-y-4">
                <input
                  type="text"
                  placeholder="Challenge Title"
                  value={customChallenge.title}
                  onChange={(e) => setCustomChallenge({...customChallenge, title: e.target.value})}
                  className="w-full p-3 rounded-lg bg-gray-900 border-2 border-gray-600 focus:border-purple-500 text-white"
                />
                <select
                  value={customChallenge.cipher}
                  onChange={(e) => setCustomChallenge({...customChallenge, cipher: e.target.value})}
                  className="w-full p-3 rounded-lg bg-gray-900 border-2 border-gray-600 focus:border-purple-500 text-white"
                >
                  <option value="caesar">Caesar</option>
                  <option value="vigenere">Vigenère</option>
                  <option value="aes">AES</option>
                  <option value="rsa">RSA</option>
                </select>
                <textarea
                  placeholder="Challenge plaintext"
                  value={customChallenge.plaintext}
                  onChange={(e) => setCustomChallenge({...customChallenge, plaintext: e.target.value})}
                  rows="3"
                  className="w-full p-3 rounded-lg bg-gray-900 border-2 border-gray-600 focus:border-purple-500 resize-none text-white"
                />
                <button
                  onClick={addCustomChallenge}
                  className="w-full bg-gradient-to-r from-purple-600 to-pink-600 text-white py-3 rounded-lg font-bold hover:shadow-xl transition-all"
                >
                  Add Challenge
                </button>
              </div>
            </div>
          </div>
        )}

        {activeTab === 'progress' && (
          <div className="space-y-6">
            <div className="bg-gray-800 rounded-xl p-6 border-2 border-gray-700">
              <h2 className="text-2xl font-bold text-yellow-400 mb-6 flex items-center">
                <Trophy className="mr-2" /> Your Progress
              </h2>
              
              <div className="grid md:grid-cols-3 gap-6 mb-8">
                <div className="bg-gradient-to-br from-purple-900 to-purple-700 p-6 rounded-xl text-center">
                  <div className="text-5xl font-bold text-yellow-400">{progress.completedCiphers.length}</div>
                  <div className="text-gray-300 mt-2">Ciphers Mastered</div>
                </div>
                <div className="bg-gradient-to-br from-blue-900 to-blue-700 p-6 rounded-xl text-center">
                  <div className="text-5xl font-bold text-green-400">{progress.completedChallenges.length}</div>
                  <div className="text-gray-300 mt-2">Challenges Completed</div>
                </div>
                <div className="bg-gradient-to-br from-pink-900 to-pink-700 p-6 rounded-xl text-center">
                  <div className="text-5xl font-bold text-cyan-400">{progress.totalAttempts}</div>
                  <div className="text-gray-300 mt-2">Total Encryptions</div>
                </div>
              </div>

              <div className="mb-8">
                <h3 className="text-xl font-bold text-yellow-400 mb-4">Mastered Ciphers</h3>
                <div className="flex flex-wrap gap-2">
                  {progress.completedCiphers.length > 0 ? (
                    progress.completedCiphers.map(cipher => (
                      <div key={cipher} className="bg-green-900 border-2 border-green-500 px-4 py-2 rounded-lg">
                        {cipher}
                      </div>
                    ))
                  ) : (
                    <p className="text-gray-400">No ciphers mastered yet. Start encrypting!</p>
                  )}
                </div>
              </div>

              <div className="text-center">
                <button
                  onClick={generateCertificate}
                  disabled={progress.completedCiphers.length < 3}
                  className="bg-gradient-to-r from-yellow-600 to-orange-600 text-white py-4 px-8 rounded-lg font-bold hover:shadow-xl transition-all disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  <Award className="inline mr-2" size={20} />
                  Generate Certificate
                  {progress.completedCiphers.length < 3 && ' (Master 3+ ciphers)'}
                </button>
              </div>
            </div>
          </div>
        )}

        {showCertificate && (
          <div className="fixed inset-0 bg-black bg-opacity-75 flex items-center justify-center z-50 p-4">
            <div className="bg-white rounded-xl p-8 max-w-2xl w-full">
              <div className="bg-gradient-to-br from-yellow-100 to-orange-100 p-8 rounded-lg border-4 border-yellow-600">
                <div className="text-center">
                  <div className="text-6xl mb-4">🏆</div>
                  <h2 className="text-4xl font-bold text-gray-900 mb-4">Certificate of Achievement</h2>
                  <p className="text-gray-600 text-lg mb-6">This certifies that</p>
                  <p className="text-3xl font-bold text-purple-700 mb-6">Crypto Master</p>
                  <p className="text-gray-600 text-lg mb-4">has successfully completed</p>
                  <p className="text-2xl font-bold text-gray-900 mb-8">Cypher Craft Pro Training</p>
                  <div className="border-t-2 border-gray-400 pt-6">
                    <p className="text-gray-600">Mastered {progress.completedCiphers.length} encryption algorithms</p>
                    <p className="text-gray-600">Rank: {progress.rank}</p>
                  </div>
                </div>
              </div>
              <div className="mt-6 grid grid-cols-2 gap-4">
                <button
                  onClick={downloadCertificate}
                  className="bg-green-600 text-white py-3 px-6 rounded-lg font-bold hover:bg-green-700 transition-all"
                >
                  <Download className="inline mr-2" size={20} />
                  Download
                </button>
                <button
                  onClick={() => setShowCertificate(false)}
                  className="bg-gray-600 text-white py-3 px-6 rounded-lg font-bold hover:bg-gray-700 transition-all"
                >
                  Close
                </button>
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default CypherCraftPro;