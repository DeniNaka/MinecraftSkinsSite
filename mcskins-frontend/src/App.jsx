import { useEffect, useState } from "react";

function App() {
    const [skins, setSkins] = useState([]);
    const [purchases, setPurchases] = useState([]);

    const total = purchases
        .reduce((sum, p) => sum + p.finalPriceUsd, 0)
        .toFixed(2);

    const loadSkins = async () => {
        try {
            const response = await fetch(`/api/skins`);
            const data = await response.json();
            setSkins(data);
        } catch (error) {
            console.error("Skin uplode Error:", error);
        }
    };

    const loadPurchases = async () => {
        try {
            const response = await fetch(`/api/purchases`);
            const data = await response.json();
            setPurchases(data);
        } catch (error) {
            console.error("Purchse uplode Error:", error);
        }
    };

    const buySkin = async (id) => {
        try {
            const response = await fetch(`/api/purchases/${id}`, {
                method: "POST"
            });

            const text = await response.text();

            if (!response.ok) {
                alert(`Purchase Error: ${response.status} - ${text}`);
                return;
            }

            alert("Skin Purchased!");
            loadPurchases();
        } catch (error) {
            console.error("Purchase Error:", error);
        } 
    };

    useEffect(() => {
        // eslint-disable-next-line react-hooks/set-state-in-effect
        loadSkins();
        loadPurchases();
    }, []);

    return (
        <div style={{ padding: 20 }}>
            <h1>Minecraft Skins Site</h1>

            <h2>Skins List:</h2>
            {skins.map(skin => (
                <div key={skin.id}>
                    <b>{skin.name}</b> |  {skin.finalPriceUsd}$ |  {skin.isAvailable ? " Available " : " Sold "} 

                    <button
                        disabled={!skin.isAvailable}
                        onClick={() => buySkin(skin.id)}
                    >
                        Buy
                    </button>
                </div>
            ))}

            <h2>Purchases</h2>
            {purchases.map(p => (
                <div key={p.id}>
                    {p.skinName} - {p.finalPriceUsd}$
                </div>
            ))}

            <h2>Total Spent</h2>
            <div>
                Final price: {total}$
            </div>
        </div>
    );
}

export default App;