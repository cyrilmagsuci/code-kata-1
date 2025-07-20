import React, { useState } from 'react'
import './App.css'

// Import our components
import SimpleCheckout from './components/SimpleCheckout'
import CheckoutHistory from './components/CheckoutHistory'
import { findProductBySku } from './utils/products'

// Define the interface for a scanned item
interface ScannedItem {
  id: string;
  sku: string;
  quantity: number;
  timestamp: Date;
  product: ReturnType<typeof findProductBySku>;
  totalAmount: number | undefined | null;
  unitPrice: number | undefined | null;
  isBundleDiscounted: boolean;
  bundleQuantity: number;
}

// Styles for the app layout
const styles = {
  container: {
    maxWidth: '1200px',
    margin: '0 auto',
    padding: '2rem',
    fontFamily: 'Arial, sans-serif',
  },
  header: {
    textAlign: 'center' as const,
    marginBottom: '2rem',
  },
  title: {
    fontSize: '2.5rem',
    color: '#333',
    marginBottom: '0.5rem',
  },
  subtitle: {
    fontSize: '1.2rem',
    color: '#666',
    marginBottom: '2rem',
  },
  footer: {
    marginTop: '3rem',
    textAlign: 'center' as const,
    color: '#666',
    fontSize: '0.9rem',
  },
};

function App() {
  // State for scanned items
  const [scannedItems, setScannedItems] = useState<ScannedItem[]>([]);
  // State for shared userSessionId
  const [userSessionId, setUserSessionId] = useState('');
  // State for the most recent totalAmount from the API
  const [latestTotalAmount, setLatestTotalAmount] = useState<number | null>(null);

  // Handler for adding a new scanned item
  const handleItemScanned = (
    itemId: string,
    sku: string,
    quantity: number,
    totalAmount: number | undefined | null,
    unitPrice: number | undefined | null,
    isBundleDiscounted: boolean,
    bundleQuantity: number
  ) => {
    const product = findProductBySku(sku);
    if (!product) {
      console.error(`Product with SKU ${sku} not found`);
      return;
    }

    const newItem: ScannedItem = {
      id: itemId,
      sku,
      quantity,
      timestamp: new Date(),
      product,
      totalAmount: totalAmount !== undefined && totalAmount !== null ? totalAmount : 0,
      unitPrice: unitPrice !== undefined && unitPrice !== null ? unitPrice : 0,
      isBundleDiscounted,
      bundleQuantity
    };

    setScannedItems(prevItems => [...prevItems, newItem]);
    
    // Update the latest totalAmount from the API
    if (totalAmount !== undefined && totalAmount !== null) {
      setLatestTotalAmount(totalAmount);
    }
  };

  return (
    <div style={styles.container}>
      <header style={styles.header}>
        <h1 style={styles.title}>Checkout System</h1>
        <p style={styles.subtitle}>
          Scan multiple items and track your checkout history
        </p>
      </header>
      
      <div style={{
        display: 'grid',
        gridTemplateColumns: '1fr 1fr',
        gap: '2rem',
        marginBottom: '2rem'
      }}>
        <div>
          <SimpleCheckout 
            onItemScanned={handleItemScanned}
            sharedUserSessionId={userSessionId}
            onUserSessionIdChange={setUserSessionId}
          />
        </div>
        <div>
          <CheckoutHistory items={scannedItems} grandTotal={latestTotalAmount} />
        </div>
      </div>
      
      <footer style={styles.footer}>
        <p>
          Implementation of the checkout system with history tracking
        </p>
      </footer>
    </div>
  )
}

export default App
