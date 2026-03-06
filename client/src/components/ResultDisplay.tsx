import React, { useState } from 'react';
import './ResultDisplay.css';

interface ResultDisplayProps {
    result: string;
}

export const ResultDisplay: React.FC<ResultDisplayProps> = ({ result }) => {
    const [copied, setCopied] = useState(false);

    const copyToClipboard = async () => {
        try {
            await navigator.clipboard.writeText(result);
            setCopied(true);
            setTimeout(() => setCopied(false), 2000);
        } catch (err) {
            console.error('Failed to copy!', err);
        }
    };

    return (
        <div className="result-container">
            <div className="result-header">
                <p className="result-label">Result</p>

                <button onClick={copyToClipboard} className="copy-button">
                    {copied ? 'Copied!' : 'Copy'}
                </button>
            </div>

            <p className="result-text">{result}</p>
        </div>
    );
};
