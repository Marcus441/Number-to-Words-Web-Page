import React from 'react';

interface SubmissionFormProps {
  amount: string;
  setAmount: (value: string) => void;
  onConvert: () => void;
  isLoading: boolean;
  error?: string;
}

export const SubmissionForm: React.FC<SubmissionFormProps> = ({
  amount,
  setAmount,
  onConvert,
  isLoading,
  error
}) => {
  return (
    <div className="space-y-4">
      <div className="relative">
        <span className="absolute left-4 top-3 text-slate-400">$</span>
        <input
          type="text"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          className={`w-full pl-8 pr-4 py-3 border rounded-lg outline-none transition-all ${error
            ? 'border-red-500 ring-1 ring-red-500'
            : 'border-slate-200 focus:ring-2 focus:ring-indigo-500'
            }`}
          placeholder="0.00"
        />
        {error && <p className="text-red-500 text-xs mt-1">{error}</p>}
      </div>

      <button
        onClick={onConvert}
        disabled={isLoading || !amount}
        className="w-full bg-indigo-600 hover:bg-indigo-700 disabled:bg-indigo-300 text-white font-medium py-3 rounded-lg transition-colors shadow-lg"
      >
        {isLoading ? 'Converting...' : 'Convert to Words'}
      </button>
    </div>
  );
};
