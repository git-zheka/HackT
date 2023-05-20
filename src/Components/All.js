import React, { useState, useEffect } from 'react';
import '../Style/style.css';

function Table() {
  const [balance, setBalance] = useState(0);
  const [cryptoEvents, setCryptoEvents] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [eventData, setEventData] = useState({
    id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    description: '',
    amount: '',
    date: '',
    type: ''
  });

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('https://hack2222.azurewebsites.net/api/Cash');
        const data = await response.json();

        setBalance(data.Balance);
        setCryptoEvents(data.CashEvents);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, []);

  const handleAddClick = () => {
    setShowForm(true);
  };

  const handleFormClose = () => {
    setShowForm(false);
  };

  const handleInputChange = (e) => {
    setEventData({ ...eventData, [e.target.name]: e.target.value });
  };

  const handleSaveClick = async () => {
    const eventDataToSend = {
      ...eventData,
      amount: parseFloat(eventData.amount) // Перетворення на тип float
    };

    try {
      const response = await fetch('https://hack2222.azurewebsites.net/api/Cash', {
        method: 'PUT',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(eventDataToSend)
      });

      if (response.ok) {
        console.log('Save button clicked');
        console.log('Request sent successfully');
        setShowForm(false);
      } else {
        console.error('Request failed with status:', response.status);
      }
    } catch (error) {
      console.error('Error sending request:', error);
    }
  };

  const handleDeleteClick = async (eventId) => {

    try {
      const response = await fetch(`https://hack2222.azurewebsites.net/api/Cash/${eventId}`, {
        method: 'DELETE'
      });
  
      if (response.ok) {
        console.log('Delete button clicked');
        console.log('Request sent successfully');
        // Оновити список cryptoEvents після видалення
        setCryptoEvents(cryptoEvents.filter(event => event.Id !== eventId));
      } else {
        console.error('Request failed with status:', response.status);
      }
    } catch (error) {
      console.error('Error sending request:', error);
    }
  };

  return (
    <div>
      <div className='btn_and_price'>
        <p2>Balance: {balance}</p2>
        <button className="add-button" onClick={handleAddClick}>Add</button>
      </div>
      <table className={showForm ? 'blur' : ''}>
        <thead>
          <tr>
            <th>Name</th>
            <th>Amount</th>
            <th>Date</th>
            <th>Type</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {cryptoEvents?.map((event) => (
            <tr key={event.Id}>
              <td>{event.Description}</td>
              <td>{event.Amount}</td>
              <td>{event.Date}</td>
              <td>{event.Type}</td>
              <td>
                <button onClick={() => handleDeleteClick(event.Id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      {showForm && (
        <div className="overlay">
          <div className="form-container">
            <h3>Add Event</h3>
            <form>
              <div className="form-group">
                <label htmlFor="name">Description</label>
                <input
                  type="text"
                  id="description"
                  name="description"
                  value={eventData.description}
                  onChange={handleInputChange}
                />
              </div>
              <div className="form-group">
                <label htmlFor="amount">Amount</label>
                <input
                  type="number"
                  id="amount"
                  name="amount"
                  value={eventData.amount}
                  onChange={handleInputChange}
                />
              </div>
              <div className="form-group">
                <label htmlFor="date">Date</label>
                <input
                  type="date"
                  id="date"
                  name="date"
                  value={eventData.date}
                  onChange={handleInputChange}
                />
              </div>
              <div className="form-group">
                <label htmlFor="type">Type</label>
                <select
                  id="type"
                  name="type"
                  value={eventData.type}
                  onChange={handleInputChange}
                >
                  <option value="">Select Type</option>
                  <option value="DEPOSIT">DEPOSIT</option>
                  <option value="WITHDRAW">WITHDRAW</option>
                </select>
              </div>
              <div className="form-buttons">
                <button type="button" onClick={handleSaveClick}>Save</button>
                <button type="button" onClick={handleFormClose}>Close</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}

export default Table;
