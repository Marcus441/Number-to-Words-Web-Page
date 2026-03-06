import React, { useState } from 'react';

interface ResultDisplayProps {
    result: string;
}

export const ResultDisplay: React.FC<ResultDisplayProps> = ({ result }) => {
    const [copied, setCopied] = useState(false);
    const copyToClipboard = async () => {
        try {
            await navigator.clipboard.writeText(result);
            setCopied(true);
            setTimeout(() => setCopied(false), 2000); // Reset after 2 seconds
        } catch (err) {
            console.error('Failed to copy!', err);
        }
    };
    return (
        <div className="mt-8 p-4 bg-indigo-50 rounded-lg border border-indigo-100 animate-in fade-in slide-in-from-top-1 relative group">
            <div className="flex justify-between items-center mb-2">
                <p className="text-xs font-bold text-indigo-600 uppercase tracking-wider">Result</p>

                <button
                    onClick={copyToClipboard}
                    className="text-[10px] bg-white border border-indigo-200 text-indigo-600 px-2 py-1 rounded hover:bg-indigo-600 hover:text-white transition-all shadow-sm font-bold uppercase"
                >
                    {copied ? 'Copied!' : 'Copy'}
                </button>
            </div>

            <p className="text-slate-800 leading-relaxed font-medium break-words pr-2">{result}</p>
        </div>
    );
};
